using System.ComponentModel.DataAnnotations;

namespace ShuffleText
{
    public enum ShuffleTextEnum_DisplayMode
    {
        [Display(Name = "同時", Description = "テキストを同時に表示")]
        Nomal = 1,

        [Display(Name = "順番", Description = "テキストを順番に表示")]
        Order = 2,
    }
}
