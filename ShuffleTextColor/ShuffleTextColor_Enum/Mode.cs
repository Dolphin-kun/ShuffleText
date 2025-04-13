using System.ComponentModel.DataAnnotations;

namespace ShuffleTextColor.ShuffleTextColor_Enum
{
    public enum Mode
    {
        [Display(Name = "ランダム", Description = "ランダム")]
        Random = 1,

        [Display(Name = "順番", Description = "順番")]
        Order = 2,
    }
}
