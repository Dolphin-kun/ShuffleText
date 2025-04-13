using ShuffleTextColor.ShuffleTextColor_Enum;
using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Controls;
using YukkuriMovieMaker.Exo;
using YukkuriMovieMaker.Player.Video;
using YukkuriMovieMaker.Plugin.Effects;
using YukkuriMovieMaker.UndoRedo;

namespace ShuffleTextColor
{
    [VideoEffect("シャッフルテキストカラー", ["アニメーション"], [], isAviUtlSupported: false)]
    public class ShuffleTextColorEffect : VideoEffectBase
    {
        public override string Label => "シャッフルテキストカラー";

        [Display(GroupName = "シャッフルテキストカラー", Name = "表示方法", Description = "色の表示方法")]
        [EnumComboBox]
        public Mode Mode_Enum { get => mode_enum; set => Set(ref mode_enum, value); }
        Mode mode_enum = Mode.Random;

        [Display(GroupName = "シャッフルテキストカラー", Name = "変化", Description ="色の変化")]
        [ToggleSlider]
        public bool Transition { get => transition; set => Set(ref transition, value); }
        bool transition = false;

        [Display(AutoGenerateField = true)]
        public ImmutableList<TransitionMode> TransitionModes { get => modes; set => Set(ref modes, value); }
        ImmutableList<TransitionMode> modes = [];


        [Display(GroupName = "アニメーションカラー", Name = "色数",Description ="アニメーションさせる色の数")]
        [Range(1, 16)]
        [TextBoxSlider("F0", "", 1, 4)]
        [DefaultValue(1)]
        public int Count { get => count; set => Set(ref count, value); }
        int count = 1;

        [Display(AutoGenerateField = true)]
        public ImmutableList<ColorValue> Colors { get => colors; set => Set(ref colors, value); }
        ImmutableList<ColorValue> colors = [new ColorValue()];


        public ShuffleTextColorEffect()
        {
            SubscribeChildUndoRedoable(Colors);
        }

        public override async ValueTask EndEditAsync()
        {
            await base.EndEditAsync();

            if (colors.Count != count)
            {
                var builder = Colors.Take(count).ToImmutableList().ToBuilder();
                for (int i = colors.Count; i < count; i++)
                {
                    builder.Add(new ColorValue());
                }
                Colors = builder.ToImmutable();
            }

            if (Transition)
            {
                if (TransitionModes.IsEmpty)
                {
                    TransitionModes = [new TransitionMode()];
                }
            }
            else
            {
                TransitionModes = [];
            }
        }

        public override IEnumerable<string> CreateExoVideoFilters(int keyFrameIndex, ExoOutputDescription exoOutputDescription)
        {
            return [];
        }

        public override IVideoEffectProcessor CreateVideoEffect(IGraphicsDevicesAndContext devices)
        {
            return new ShuffleTextColorEffectProcessor(devices, this);
        }

        protected override IEnumerable<IAnimatable> GetAnimatables() => TransitionModes;
    }


    public class ColorValue : UndoRedoable
    {
        [Display(GroupName = "アニメーションカラー", Name = "色",Description ="色")]
        [ColorPicker]
        public Color Color { get => color; set => Set(ref color, value); }
        Color color = Colors.White;
    }

    public class TransitionMode : Animatable
    {
        [Display(GroupName = "シャッフルテキストカラー", Name = "間隔", Description = "文字色が変化する間隔")]
        [AnimationSlider("F2", "秒", 0, 0.25)]
        public Animation Interval { get; } = new Animation(0, 0, 1000.00);

        protected override IEnumerable<IAnimatable> GetAnimatables() => [Interval];
    }
}
