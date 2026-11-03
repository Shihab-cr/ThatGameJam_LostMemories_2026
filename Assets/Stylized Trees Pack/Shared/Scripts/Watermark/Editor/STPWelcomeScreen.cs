using UnityEngine;
using UnityEditor;
using laplahce.Watermark.Core;
using laplahce.Watermark.Widgets;

namespace laplahce.Watermark.Editor
{
    internal static class STPWelcomeScreen
    {
        private const string REVIEW_URL = "https://assetstore.unity.com/packages/slug/261728#reviews";

        private const string UPGRADE_HDRP_GUID = "f78d1dc2ab0d0c243a3be4e4a18e920a";
        private const string UPGRADE_URP_GUID = "ca0aea6caa1c6704b9fc09cd1c1a6348";
        private const string UPGRADE_BUILTIN_GUID = "8ca70e58cb8096546838e55c784fac0d";

        private static readonly WatermarkWindowConfig CONFIG = new WatermarkWindowConfig
        {
            PackName = "Stylized Tree Pack",
            Slug = "STP",
            PackVersion = "2.0",
            Tagline = "A pack of stylized trees & bushes.",
            DontShowAgainKey = "STP_Watermark_DontShow",
            MenuPath = "Tools/STP/Welcome Screen",
            MenuPriority = 1,
            Size = new Vector2(410f, 520f)
        };

        [MenuItem("Tools/STP/Welcome Screen", priority = 1)]
        private static void OpenFromMenu()
        {
            WatermarkWindowLauncher.Show(CONFIG, Build);
        }

        [InitializeOnLoadMethod]
        private static void ShowOnStartup()
        {
            string sessionKey = CONFIG.Slug + "_ShownThisSession";
            if (SessionState.GetBool(sessionKey, false)) return;
            SessionState.SetBool(sessionKey, true);

            EditorApplication.delayCall += () => WatermarkWindowLauncher.ShowIfNotDismissed(CONFIG, Build);
        }

        private static WatermarkWindow Build(WatermarkWindow window) => window
            .Tab("Home")
                .Section("Thanks for picking up " + CONFIG.Slug)
                .Space(6)
                .Text($"{CONFIG.Slug} is a pack of stylized trees & bushes. This screen covers setup, render pipeline upgrades & the resources you'll want next.")
                .Space(10)
                .ReviewCard("Enjoying it so far? A review helps a lot.", "★  Rate the Pack", REVIEW_URL)
                .Space(10).Line().Space(10)
                .Section("Recommended First Steps")
                .Space(6)
                .LinkCardToTab("1. Upgrade to your render pipeline", "Go to Setup  →", "Setup", "Match the pack to URP, HDRP or Built-In.")
                .Space(4)
                .LinkCardToTab("2. Read the 5-minute intro", "Go to Resources  →", "Resources", "Fastest way to read the whole workflow.")
                .Space(10).Line().Space(10)
                .ButtonRow(
                    new GradientButtonWidget("📖  Documentation", () => Application.OpenURL("https://laplahce.com/docs/stylized-tree-pack")),
                    new GradientButtonWidget("📝  Report a bug", () => Application.OpenURL("https://laplahce.com/contact")),
                    new GradientButtonWidget("💬  Support", () => Application.OpenURL("https://laplahce.com/contact")))
            .Tab("Setup")
                .Section("Render Pipeline")
                .Space(4)
                .Text("Switch the pack's shaders to match your RP. Confirm the target pipeline is already installed before upgrading.")
                .Space(6)
                .ButtonRow(
                    ImportButtonWidget("⬆  HDRP", "Upgrade to HDRP", "This will import the HDRP upgrade package.\nMake sure HDRP is installed in the Package Manager.", UPGRADE_HDRP_GUID),
                    ImportButtonWidget("⬆  URP", "Upgrade to URP", "This will import the URP upgrade package.\nMake sure URP is installed in the Package Manager.", UPGRADE_URP_GUID),
                    ImportButtonWidget("⬆  Built-In", "Upgrade to Built-In", "This will import the Built-In render pipeline upgrade package.", UPGRADE_BUILTIN_GUID))
                .Space(10).Line().Space(10)
                .ButtonRow(
                    new GradientButtonWidget("📖  Documentation", () => Application.OpenURL("https://laplahce.com/docs/stylized-tree-pack")),
                    new GradientButtonWidget("📝  Report a bug", () => Application.OpenURL("https://laplahce.com/contact")),
                    new GradientButtonWidget("💬  Support", () => Application.OpenURL("https://laplahce.com/contact")))
            .Tab("Resources")
                .Section("Learn the Pack")
                .Space(4)
                .Text("Short videos and written docs, most under 5 minutes.")
                .Space(6)
                .LinkCard("Installation", "Open ↗", "https://laplahce.com/docs/stylized-tree-pack/installation", "Make sure you installed everything correctly.")
                .Space(4)
                .LinkCard("Getting Started", "Open ↗", "https://laplahce.com/docs/stylized-tree-pack/getting-started", "Get started using the pack in under 5 minutes.")
                .Space(4)
                .LinkCard("Full Documentation", "Open ↗", "https://laplahce.com/docs/stylized-tree-pack", "Written guides & documentation.")
                .Space(10).Line().Space(10)
                .Section("More of my packages")
                .Space(4)
                .Text("Check out some of my other packages.")
                .Space(6)
                .LinkCard("Bush & Foliage Creator", "View →", "https://assetstore.unity.com/packages/slug/245037")
                .Space(4)
                .LinkCard("Obstacle Course Kit", "View →", "https://assetstore.unity.com/packages/slug/293703")
                .Space(4)
                .LinkCard("Stylized Rocks Pack", "View →", "https://assetstore.unity.com/packages/slug/328969")
                .Space(10).Line().Space(10)
                .ButtonRow(
                    new GradientButtonWidget("📖  Documentation", () => Application.OpenURL("https://laplahce.com/docs/stylized-tree-pack")),
                    new GradientButtonWidget("📝  Report a bug", () => Application.OpenURL("https://laplahce.com/contact")),
                    new GradientButtonWidget("💬  Support", () => Application.OpenURL("https://laplahce.com/contact")));

        private static PackageImportButtonWidget ImportButtonWidget(string label, string dialogTitle, string dialogMessage, string packageGuid) =>
            new PackageImportButtonWidget(label, dialogTitle, dialogMessage, packageGuid, CONFIG.PackName);
    }
}