using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Controls;
using YukkuriMovieMaker.Exo;
using YukkuriMovieMaker.ItemEditor.CustomVisibilityAttributes;
using YukkuriMovieMaker.Player.Video;
using YukkuriMovieMaker.Plugin.Effects;

namespace ShuffleText
{
    [VideoEffect("テキストをシャッフルしながら登場退場", ["登場退場"], ["Shuffle Text","シャッフルテキスト","shahhuru tekisuto"], isAviUtlSupported: false)]
    internal class ShuffleTextEffect : VideoEffectBase
    {
        public override string Label
        {
            get
            {
                if (EffectEnter && !EffectExit)
                    return "テキストをシャッフルしながら登場";
                if (!EffectEnter && EffectExit)
                    return "テキストをシャッフルしながら退場";
                if (!EffectEnter && !EffectExit)
                    return "シャッフルテキスト";
                return "テキストをシャッフルしながら登場退場";
            }
        }

        //登場退場
        [Display(GroupName = "登場退場", Name = "登場時", Description = "アイテムが登場する際にエフェクトを適用する")]
        [ToggleSlider]
        public bool EffectEnter
        {
            get => effectEnter;
            set
            {
                if (Set(ref effectEnter, value))
                {
                    OnPropertyChanged(nameof(Label));
                }
            }
        }
        bool effectEnter = true;

        [Display(GroupName = "登場退場", Name = "退場時", Description = "アイテムが退場する際にエフェクトを適用する")]
        [ToggleSlider]
        public bool EffectExit
        {
            get => effectExit;
            set
            {
                if (Set(ref effectExit, value))
                {
                    OnPropertyChanged(nameof(Label));
                }
            }
        }
        bool effectExit = true;

        [Display(GroupName = "登場退場", Name ="効果時間",Description ="エフェクトが再生される秒数")]
        [TextBoxSlider("F2", "秒", 0, 0.5 )]
        [DefaultValue(0d)]
        [Range(0, 99999d)]
        public double T { get => time; set => Set(ref time, value); }
        double time = 0.30;

        [Display(GroupName = "登場退場", Name = "間隔", Description = "間隔")]
        [AnimationSlider("F2", "秒", 0, 0.25)]
        public Animation Inverval { get; } = new Animation(0, 0, 1000.00);

        [Display(GroupName = "登場退場", Name = "ずれ", Description = "ずれ")]
        [ToggleSlider]
        public bool Gap { get => gap; set => Set(ref gap, value); }
        bool gap = false;

        [Display(GroupName = "登場退場", Name = "表示方法", Description = "テキストの表示方法")]
        [EnumComboBox]
        public ShuffleTextEnum_DisplayMode Enum_DisplayMode { get => mode_displayEnum; set => Set(ref mode_displayEnum, value); }
        ShuffleTextEnum_DisplayMode mode_displayEnum = ShuffleTextEnum_DisplayMode.Nomal;

        [Display(GroupName = "登場退場", Name = "開始時間", Description = "テキストの表示開始時間")]
        [TextBoxSlider("F2", "秒", 0, 0.5)]
        [DefaultValue(0d)]
        [Range(0, 99999d)]
        [ShowPropertyEditorWhen(nameof(Enum_DisplayMode), ShuffleTextEnum_DisplayMode.Order)]
        public double DisplayStartTime { get => startTime; set => Set(ref startTime, value); }
        double startTime = 0.30;

        //アニメーションテキスト
        [Display(GroupName ="アニメーションテキスト",Name ="表示テキスト",Description ="表示されるテキスト")]
        [EnumComboBox]
        public ShuffleTextEnum_Mode Enum_Mode { get => mode_Enum; set => Set(ref mode_Enum, value); }
        ShuffleTextEnum_Mode mode_Enum = ShuffleTextEnum_Mode.Alphabet;

        //Custom
        [Display(GroupName = "アニメーションテキスト", Name = "A-Z", Description = "アルファベット大文字")]
        [ToggleSlider]
        [ShowPropertyEditorWhen(nameof(Enum_Mode), ShuffleTextEnum_Mode.Custom)]
        public bool UpLetter { get => upletter; set => Set(ref upletter, value); }
        bool upletter = false;

        [Display(GroupName = "アニメーションテキスト", Name = "a-z", Description = "アルファベット小文字")]
        [ToggleSlider]
        [ShowPropertyEditorWhen(nameof(Enum_Mode), ShuffleTextEnum_Mode.Custom)]
        public bool LowLetter { get => lowletter; set => Set(ref lowletter, value); }
        bool lowletter = false;
        [Display(GroupName = "アニメーションテキスト", Name = "ひらがな", Description = "ひらがな")]
        [ToggleSlider]
        [ShowPropertyEditorWhen(nameof(Enum_Mode), ShuffleTextEnum_Mode.Custom)]
        public bool Hirakana { get => hirakana; set => Set(ref hirakana, value); }
        bool hirakana = false;
        [Display(GroupName = "アニメーションテキスト", Name = "カタカナ", Description = "カタカナ")]
        [ToggleSlider]
        [ShowPropertyEditorWhen(nameof(Enum_Mode), ShuffleTextEnum_Mode.Custom)]
        public bool Katakana { get => katakana; set => Set(ref katakana, value); }
        bool katakana = false;
        [Display(GroupName = "アニメーションテキスト", Name = "漢字", Description = "漢字")]
        [ToggleSlider]
        [ShowPropertyEditorWhen(nameof(Enum_Mode), ShuffleTextEnum_Mode.Custom)]
        public bool Kanji { get => kanji; set => Set(ref kanji, value); }
        bool kanji = false;
        [Display(GroupName = "アニメーションテキスト", Name = "数字", Description = "数字")]
        [ToggleSlider]
        [ShowPropertyEditorWhen(nameof(Enum_Mode), ShuffleTextEnum_Mode.Custom)]
        public bool Number { get => num; set => Set(ref num, value); }
        bool num = false;
        [Display(GroupName = "アニメーションテキスト", Name = "記号", Description = "記号")]
        [ToggleSlider]
        [ShowPropertyEditorWhen(nameof(Enum_Mode), ShuffleTextEnum_Mode.Custom)]
        public bool Symbol { get => symbol; set => Set(ref symbol, value); }
        bool symbol = false;
        [Display(GroupName = "アニメーションテキスト", Name = "テキスト", Description = "シャッフルするテキスト")]
        [TextEditor(AcceptsReturn = true)]
        [ShowPropertyEditorWhen(nameof(Enum_Mode), ShuffleTextEnum_Mode.Custom)]
        public string Text { get => text; set => Set(ref text, value); }
        string text = string.Empty;


        [Display(GroupName = "アニメーションテキスト", Name = "フォント", Description = "フォント")]
        [FontComboBox]
        public string Font { get => font; set => Set(ref font, value); }
        string font = "メイリオ";

        [Display(GroupName = "アニメーションテキスト", Name = "サイズ", Description = "サイズ")]
        [AnimationSlider("F1", "px", 1.0, 50.0)]
        public Animation FontSize { get; } = new Animation(34.0, 1, 100000.0);

        [Display(GroupName = "アニメーションテキスト", Name = "文字色", Description = "文字の色")]
        [ColorPicker]
        public Color Color { get => color; set => Set(ref color, value); }
        Color color = Colors.White;

        [Display(GroupName = "アニメーションテキスト", Name = "太字", Description = "太文字")]
        [ToggleSlider]
        public bool Bold { get => bold; set => Set(ref bold, value); }
        bool bold = false;
        [Display(GroupName = "アニメーションテキスト", Name = "イタリック", Description = "イタリック")]
        [ToggleSlider]
        public bool Italic { get => italic; set => Set(ref italic, value); }
        bool italic = false;


        public override IEnumerable<string> CreateExoVideoFilters(int keyFrameIndex, ExoOutputDescription exoOutputDescription)
        {
            return [];
        }

        public override IVideoEffectProcessor CreateVideoEffect(IGraphicsDevicesAndContext devices)
        {
            return new ShuffleTextEffectProcessor(devices, this);
        }

        protected override IEnumerable<IAnimatable> GetAnimatables() => [FontSize];
    }
}
