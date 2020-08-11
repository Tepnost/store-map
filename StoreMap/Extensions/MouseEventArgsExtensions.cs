using Microsoft.AspNetCore.Components.Web;
using StoreMap.Data.Dtos;

namespace StoreMap.Extensions
{
    public static class MouseEventArgsExtensions
    {
        public static int GetXInDiv(this MouseEventArgs e, RectPosition div)
        {
            return (int) e.ClientX - div.Left;
        }
        
        public static int GetYInDiv(this MouseEventArgs e, RectPosition div)
        {
            return (int) e.ClientY - div.Top;
        }
    }
}