using ShuffleTextColor.ShuffleTextColor_Enum;
using Vortice.Direct2D1;
using Vortice.Direct2D1.Effects;
using Vortice.Mathematics;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Player.Video;

namespace ShuffleTextColor
{
    internal class ShuffleTextColorEffectProcessor : IVideoEffectProcessor
    {
        private readonly IGraphicsDevicesAndContext devices;
        readonly ShuffleTextColorEffect item;
        readonly DisposeCollector disposer = new();

        readonly AlphaMask alphaMask;

        public ID2D1Image Output { get; }

        ID2D1Image? input;
        ID2D1CommandList? commandList;

        public ShuffleTextColorEffectProcessor(IGraphicsDevicesAndContext devices, ShuffleTextColorEffect item)
        {
            this.devices = devices;
            this.item = item;

            alphaMask = new AlphaMask(devices.DeviceContext);
            disposer.Collect(alphaMask);
            Output = alphaMask.Output;
            disposer.Collect(Output);
        }


        public DrawDescription Update(EffectDescription effectDescription)
        {
            var frame = effectDescription.ItemPosition.Frame;
            var length = effectDescription.ItemDuration.Frame;
            var fps = effectDescription.FPS;

            var textIndex = effectDescription.InputIndex;
            var interval = 0.00;

            if (!item.TransitionModes.IsEmpty)
                interval = item.TransitionModes[0].Interval.GetValue(frame, length, fps) * fps;

            int seed = 0;

            if (item.Mode_Enum == Mode.Order)
            {
                int section;
                if ((int)interval == 0)
                    section = frame;
                else
                    section = frame / (int)interval;
                int num = item.Transition ? section : 1;
                seed = (num + textIndex) % item.Colors.Count;
            }
            else
            {
                int section;
                if ((int)interval == 0)
                    section = frame;
                else
                    section = frame / (int)interval;
                int num = item.Transition ? ((section + textIndex) * (section + textIndex)) + textIndex : ((1000 + textIndex) * (1000 + textIndex)) + textIndex;
                seed = new Random(num).Next() % item.Colors.Count;
            }

            var color = item.Colors[seed].Color;
            var dc = devices.DeviceContext;
            var inputRect = dc.GetImageLocalBounds(input);
            using var brush = dc.CreateSolidColorBrush(new Color(color.R, color.G, color.B, color.A));

            if (commandList != null)
                disposer.RemoveAndDispose(ref commandList);
            commandList = dc.CreateCommandList();
            disposer.Collect(commandList);
            dc.Target = commandList;
            dc.BeginDraw();
            dc.Clear(null);
            dc.FillRectangle(inputRect, brush);
            dc.EndDraw();
            dc.Target = null;
            commandList.Close();
            alphaMask.SetInput(0, commandList, true);

            return effectDescription.DrawDescription;
        }


        public void ClearInput()
        {
            alphaMask.SetInput(0, null, true);
            alphaMask.SetInput(1, null, true);
        }

        public void SetInput(ID2D1Image? input)
        {
            this.input = input;
            alphaMask.SetInput(1, input, true);
        }

        public void Dispose()
        {
            disposer.Dispose();
        }
    }
}
