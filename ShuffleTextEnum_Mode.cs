using System.ComponentModel.DataAnnotations;

namespace ShuffleText
{
    public enum ShuffleTextEnum_Mode
    {
        [Display(Name = "アルファベット(A-z)", Description = "アルファベット(大文字・小文字)")]
        Alphabet = 1,

        [Display(Name = "数値(0-9)", Description = "数値(0-9)")]
        Number = 2,

        [Display(Name = "記号", Description = "記号")]
        Symbol = 3,

        [Display(Name = "カスタム", Description = "カスタム")]
        Custom = 4,
    }
}
