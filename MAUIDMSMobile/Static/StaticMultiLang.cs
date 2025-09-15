using System.Globalization;
using LocalizationResourceManager.Maui; // ✅ Le bon package de JorgeFraga


namespace MAUIDMSMobile.Static
{
    public static class StaticMultiLang
    {
        public static ILocalizationResourceManager LocalizationResourceManager { get; set; }
        public static FlowDirection MLFlowDirection { get; private set; } = FlowDirection.LeftToRight;

        public static TextAlignment MLHorizontalAlign = TextAlignment.Start;

        public static TextAlignment MLHorizontalTextAlign = TextAlignment.Start;




        public static LayoutOptions MLLayoutOption = LayoutOptions.Start;
        public static LayoutOptions MLLayoutOptionWithExpand = LayoutOptions.StartAndExpand;
        private static void SetMLFlowDirection(FlowDirection flowDirection)
        {
            try
            {
                MLFlowDirection = flowDirection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void SetMLTextAlign(TextAlignment textAlignment)
        {
            try
            {
                MLHorizontalTextAlign = textAlignment;
                MLHorizontalAlign = textAlignment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void SetMLHorizontalTextAlign(TextAlignment textAlignment)
        {
            try
            {
                MLHorizontalTextAlign = TextAlignment.Center;
                MLHorizontalTextAlign = textAlignment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private static void SetHorizontalOption(LayoutOptions layoutOpt, LayoutOptions layoutOptWithExpand)
        {
            try
            {
                MLLayoutOption = layoutOpt;
                MLLayoutOptionWithExpand = layoutOptWithExpand;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SetAppLanguage(string lang, ILocalizationResourceManager localizationResourceManager)
        {
            try
            {
                switch (lang)
                {
                    case "fr":
                    case "FR":
                        localizationResourceManager.CurrentCulture = new CultureInfo("fr");
                        SetMLFlowDirection(FlowDirection.LeftToRight);
                        SetMLTextAlign(TextAlignment.Start);
                        SetMLHorizontalTextAlign(TextAlignment.Start);
                        SetHorizontalOption(LayoutOptions.Start, LayoutOptions.Start);
                        //SetMLAllPickersImageAlign(ImageAlignment.Right);
                        break;

                    case "en":
                    case "EN":
                        localizationResourceManager.CurrentCulture = new CultureInfo("en");
                        SetMLFlowDirection(FlowDirection.LeftToRight);
                        SetMLTextAlign(TextAlignment.Start);
                        SetMLHorizontalTextAlign(TextAlignment.Start);
                        SetHorizontalOption(LayoutOptions.Start, LayoutOptions.Start);
                        //SetMLAllPickersImageAlign(ImageAlignment.Right);
                        break;

                    case "ar":
                    case "AR":
                        localizationResourceManager.CurrentCulture = new CultureInfo("ar");
                        SetMLFlowDirection(FlowDirection.RightToLeft);
                        SetMLTextAlign(TextAlignment.End);
                        SetMLHorizontalTextAlign(TextAlignment.Start);
                        SetHorizontalOption(LayoutOptions.End, LayoutOptions.End);
                        //SetMLAllPickersImageAlign(ImageAlignment.Left);
                        break;


                    default: //français
                        localizationResourceManager.CurrentCulture = new CultureInfo("fr");
                        SetMLFlowDirection(FlowDirection.LeftToRight);
                        SetMLTextAlign(TextAlignment.Start);
                        SetMLHorizontalTextAlign(TextAlignment.Start);
                        SetHorizontalOption(LayoutOptions.Start, LayoutOptions.Start);
                        //SetMLAllPickersImageAlign(ImageAlignment.Right);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
