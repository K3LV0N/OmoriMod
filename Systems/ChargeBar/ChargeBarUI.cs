using OmoriMod.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using ReLogic.Content;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Systems.ChargeBar
{
    public class ChargeBarUI : UIState
    {
        private UIText text;

        private UIElement area;

        private UIImage barFrame;

        private static Texture2D barTexture;

        private static Texture2D barTextureFull;
        public override void OnInitialize()
        {
            area = new UIElement();
            area.Top.Set(-25, 0f);
            area.Left.Set(64, 0f);
            area.Width.Set(182, 0f);
            area.Height.Set(60, 0f);
            area.HAlign = area.VAlign = 0.5f;


            barFrame = new UIImage(ModContent.Request<Texture2D>("OmoriMod/Systems/ChargeBar/ChargeBarFrame"));

            barTexture = ModContent.Request<Texture2D>("OmoriMod/Systems/ChargeBar/ChargeBarMiddle", AssetRequestMode.ImmediateLoad).Value;
            barTextureFull = ModContent.Request<Texture2D>("OmoriMod/Systems/ChargeBar/ChargeBarFull", AssetRequestMode.ImmediateLoad).Value;

            barFrame.Left.Set(0, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(138, 0f);
            barFrame.Height.Set(34, 0f);

            text = new UIText("", 1.5f);
            text.Left.Set(138, 0f);
            text.Top.Set(34, 0f);
            text.Width.Set(40, 0f);
            text.Height.Set(0, 0f);

            area.Append(text);
            area.Append(barFrame);
            Append(area);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if( !(Main.LocalPlayer.HeldItem.ModItem is FocusItem))
            {
                return;
            }

            // typically base functions are empty, but not this one
            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            DrawChargeBar(spriteBatch);
        }

        private void DrawChargeBar(SpriteBatch spriteBatch)
        {
            FocusPlayer modPlayer = Main.LocalPlayer.GetModPlayer<FocusPlayer>();
            Player player = modPlayer.Player;

            // Calculate percent full
            float percentage = (float)((float)(modPlayer.currentCharge) / (float)(modPlayer.maxCharge));

            // make sure percent doesn't go above 1
            percentage = Utils.Clamp(percentage, 0f, 1f);

            // Here we get the screen dimensions of the barFrame element,
            // then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient.
            Rectangle InnerBar = barFrame.GetInnerDimensions().ToRectangle();
            InnerBar.X += 4;
            InnerBar.Width -= 94;
            InnerBar.Y += 3;
            InnerBar.Height -= 21;

            if (percentage == 1f)
            {
                spriteBatch.Draw(barTextureFull, InnerBar, Color.White * 1);
            }

            int currentWidth = (int)(InnerBar.Width * percentage);
            if (currentWidth > 0 && percentage != 1f)
            {
                
                int height = barTexture.Height;
                Rectangle InnerBarPartial = new Rectangle(InnerBar.X, InnerBar.Y, currentWidth, InnerBar.Height);
                Texture2D currentTexture = new Texture2D(barTexture.GraphicsDevice, currentWidth, height);

                Color[] data = new Color[currentWidth * height];
                Color[] originalData = new Color[barTexture.Width * height];
                barTexture.GetData(originalData);

                // copy texture until new width
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < currentWidth; x++)
                    {
                        // Copy pixels, excluding the rightmost pixels
                        data[y * currentWidth + x] = originalData[y * barTexture.Width + x];
                    }
                }

                currentTexture.SetData(data);  

                spriteBatch.Draw(currentTexture, InnerBarPartial, Color.White * 1);
                
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!(Main.LocalPlayer.HeldItem.ModItem is FocusItem))
            {
                return;
            }
            // Setting the text per tick to update and show our resource values.
            text.SetText($"");
            
            // typically base functions are empty, but not this one
            base.Update(gameTime);
        }
    }
}
