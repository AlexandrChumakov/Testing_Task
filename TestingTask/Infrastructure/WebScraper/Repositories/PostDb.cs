using Dapper;
using Npgsql;

namespace Infrastructure.WebScraper.Repositories;

public class PostDb(NpgsqlConnection npgsqlConnection)
{
    public async Task CreateTablesAsync()
    {
        const string sql =
            @"CREATE TABLE IF NOT EXISTS public.posts(
            title VARCHAR(2500) NOT NULL, 
            date DATE, 
            description varchar(5000) NOT NULL
        );
        ALTER TABLE posts OWNER TO postgres;

        CREATE OR REPLACE FUNCTION get_top_ten()
        RETURNS JSONB AS
        $$
        DECLARE
            concat_desc TEXT;
            lowercase   TEXT;
            splited     TEXT[];
            result      JSONB;
            stopWords   TEXT[] := array['да', 'нет', 'что', 'если', ',', 'тот', 'этот', 'а', 'он', 'и', ' ', '', 'на', 'с', 'в', 'за', 'как', 'об', '-', 'из', 'ну', 'типа', 'короче', 'значит', 'это', 'вот',
                'просто', 'ещё', 'там', 'вроде', 'скажем', 'понимаешь', 'собственно', 'так сказать', 'как бы',
                'между прочим', 'так вот', 'к', '–', 'не', 'от', 'по', 'для', 'его', 'фото', 'более', '.', ':',
                ';', '!', '?', '(', ')', '[', ']', '{', '}', '\\', '""', '/', '\\\\', '|', '&', '*', '^', '%', '$',
                '#', '@', '~', '', '<', '>', '=', '+', '_', '6', 'о', 'у', 'до', 'со'];
        BEGIN
            SELECT STRING_AGG(description, ' ')
            INTO concat_desc
            FROM posts;

            IF concat_desc IS NULL THEN
                RETURN '[]'::JSONB;
            END IF;

            SELECT LOWER(concat_desc)
            INTO lowercase;

            SELECT REGEXP_SPLIT_TO_ARRAY(lowercase, '[, .:]+')
            INTO splited;

            SELECT ARRAY_AGG(word)
            INTO splited
            FROM unnest(splited) AS word
            WHERE word != ALL (stopWords);

            SELECT jsonb_agg(word)
            INTO result
            FROM (SELECT word
                  FROM unnest(splited) AS word
                  GROUP BY word
                  ORDER BY COUNT(*) DESC
                  LIMIT 10) AS top_words;

            RETURN result;

        END;
        $$ LANGUAGE plpgsql;

        CREATE OR REPLACE FUNCTION get_sorted_posts(fromDate TIMESTAMP WITHOUT TIME ZONE, toDate TIMESTAMP WITHOUT TIME ZONE)
        RETURNS SETOF posts AS
        $$
        BEGIN
            RETURN QUERY
                SELECT *
                FROM posts p
                WHERE p.date >= fromDate::DATE AND p.date <= toDate::DATE
                ORDER BY p.date ASC;
        END;
        $$ LANGUAGE plpgsql;

        CREATE OR REPLACE PROCEDURE put_posts(p_title VARCHAR(2500), p_post_date timestamp without time zone, p_description VARCHAR(5000))
        LANGUAGE plpgsql
        AS $$
        BEGIN
            INSERT INTO posts(title, date, description)
            VALUES (p_title, p_post_date::date, p_description);
        END;
        $$;
        
        CREATE OR replace FUNCTION contains_in_posts(value varchar(5000))
    RETURNS SETOF posts AS
$$
BEGIN
    return query 
    select *
    from posts
    where posts.description like '%' || value || '%';
END;
$$ LANGUAGE plpgsql;
        ";

        await npgsqlConnection.ExecuteAsync(sql);
    }
}