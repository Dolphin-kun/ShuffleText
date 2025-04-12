using System.ComponentModel.DataAnnotations;

namespace ShuffleTextInOut.ShuffleTextInOut_Enum
{
    public enum DisplayMode
    {
        [Display(Name = "同時", Description = "アニメーションテキストを同時に表示")]
        Nomal = 1,

        [Display(Name = "順番", Description = "アニメーションテキストを順番に表示")]
        Order = 2,
    }
}
