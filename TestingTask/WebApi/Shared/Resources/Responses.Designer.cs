﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestingTask.WebApi.Shared.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Responses {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Responses() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("TestingTask.WebApi.Shared.Resources.Responses", typeof(Responses).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string Required_phone {
            get {
                return ResourceManager.GetString("Required_phone", resourceCulture);
            }
        }
        
        internal static string Required_pass {
            get {
                return ResourceManager.GetString("Required_pass", resourceCulture);
            }
        }
        
        internal static string Format_phone {
            get {
                return ResourceManager.GetString("Format_phone", resourceCulture);
            }
        }
        
        internal static string Format_pass {
            get {
                return ResourceManager.GetString("Format_pass", resourceCulture);
            }
        }
        
        internal static string Compare_wrong {
            get {
                return ResourceManager.GetString("Compare_wrong", resourceCulture);
            }
        }
        
        internal static string Invalid_user_data {
            get {
                return ResourceManager.GetString("Invalid user data", resourceCulture);
            }
        }
    }
}
