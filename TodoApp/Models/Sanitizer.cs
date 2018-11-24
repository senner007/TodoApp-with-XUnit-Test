using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Ganss.XSS;



namespace TodoApp
{
    // https://nugetmusthaves.com/Tag/AntiXSS
    // https://github.com/mganss/HtmlSanitizer
    public static class HtmlSanitize
    {
          private static HtmlSanitizer sanitizer { get; set; } = new HtmlSanitizer();
          public static Todo Sanitize(Todo todo) {

            
               return new Todo { 
                Name = SanitizeString(todo.Name), 
                Checkmark = todo.Checkmark, 
            };

          }
          public static string SanitizeString(string s) => sanitizer.Sanitize(s);
                
    }

}