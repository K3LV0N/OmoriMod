using OmoriMod.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Systems.ChargeBar
{
    public class ChargeBarUI : UIState
    {
        private UIText text;

        private UIElement area;

        private UIImage barFrame;

        private Color gradientA;

        private Color gradientB;
        public override void OnInitialize()
        {
            area = new UIElement();
            area.Top.Set(-25, 0f);
            area.Left.Set(64, 0f);
            area.Width.Set(182, 0f);
            area.Height.Set(60, 0f);
            area.HAlign = area.VAlign = 0.5f;


            barFrame = new UIImage(Request<Texture2D>("OmoriMod/Systems/ChargeBar/ChargeBarFrame"));
            

            barFrame.Left.Set(0, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(138, 0f);
            barFrame.Height.Set(34, 0f);

            text = new UIText("", 1.5f);
            text.Left.Set(138, 0f);
            text.Top.Set(34, 0f);
            text.Width.Set(40, 0f);
            text.Height.Set(0, 0f);

            gradientA = new Color(245, 59, 59); // A pinky red
            gradientB = new Color(90, 255, 71); // A light green

            area.Append(text);
            area.Append(barFrame);
            Append(area);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if( !(Main.LocalPlayer.HeldItem.ModItem is FocusClass))
            {
                return;
            }

            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<OmoriPlayer>();
            // Calculate quotient
            float quotient = (float)( (float)(modPlayer.currentCharge) / (float)(modPlayer.maxCharge) ); // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 4;
            hitbox.Width -= 93;
            hitbox.Y += 5;
            hitbox.Height -= 21;

            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                //float percent = (float)i / steps; // Alternate Gradient Approach
                float percent = (float)i / (right - left);
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height - 3), Color.Lerp(gradientA, gradientB, percent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!(Main.LocalPlayer.HeldItem.ModItem is FocusClass))
            {
                return;
            }
            // Setting the text per tick to update and show our resource values.
            text.SetText($"");
            base.Update(gameTime);
        }
    }
}
