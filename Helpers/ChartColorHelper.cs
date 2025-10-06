using SkiaSharp;

namespace DefBTGBrown.Helpers
{
    public static class ChartColorHelper
    {
        //Só uma forma rápida de não repetir cores na distribuição das linhas no chart
        public static SKColor GetRandomColor()
        {
            Random random = new();

            return random.Next(1, 37) switch
            {
                1 => SKColors.Red,
                2 => SKColors.Tomato,
                3 => SKColors.Orange,
                4 => SKColors.Coral,
                5 => SKColors.HotPink,
                6 => SKColors.DeepPink,
                7 => SKColors.Firebrick,
                8 => SKColors.Yellow,
                9 => SKColors.Gold,
                10 => SKColors.OrangeRed,
                11 => SKColors.Chocolate,
                12 => SKColors.DarkKhaki,
                13 => SKColors.Green,
                14 => SKColors.LimeGreen,
                15 => SKColors.SpringGreen,
                16 => SKColors.ForestGreen,
                17 => SKColors.OliveDrab,
                18 => SKColors.LightSeaGreen,
                19 => SKColors.Teal,
                20 => SKColors.Cyan,
                21 => SKColors.Aqua,
                22 => SKColors.Turquoise,
                23 => SKColors.DarkCyan,
                24 => SKColors.Blue,
                25 => SKColors.DodgerBlue,
                26 => SKColors.DeepSkyBlue,
                27 => SKColors.RoyalBlue,
                28 => SKColors.SteelBlue,
                29 => SKColors.MediumBlue,
                30 => SKColors.Purple,
                31 => SKColors.Indigo,
                32 => SKColors.DarkViolet,
                33 => SKColors.Orchid,
                34 => SKColors.Magenta,
                35 => SKColors.MediumPurple,
                36 => SKColors.SlateBlue,
                _ => SKColors.DodgerBlue
            };
        }
    }
}
