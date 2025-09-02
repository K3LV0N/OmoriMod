﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OmoriMod.Content.Players;
using OmoriMod.Content.Projectiles.Friendly.Arrows.Tier2.NoDrops;
using OmoriMod.Content.Projectiles.Friendly.Melee.Bat;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace OmoriMod.Systems.AbilitySystem.AbilityMenuUI
{
    public class AbilityMenu : UIState
    {
        private UIElement area;

        private UIImage menu;

        private UIImageButton confirm;
        private UIImageButton cancel;
        private UIImageButton arrow;
        private UIImageButton phantom;

        private int _selectedPassiveID = -1;

        private void Confirm(UIMouseEvent evt, UIElement listeningElement) {

            if (_selectedPassiveID != -1)
            {
                Main.LocalPlayer.GetModPlayer<AbilityPlayer>().projectileID = _selectedPassiveID;
            }
            Main.LocalPlayer.GetModPlayer<AbilityPlayer>().abilityMenuActive = false;
        }

        private void Cancel(UIMouseEvent evt, UIElement listeningElement) { Main.LocalPlayer.GetModPlayer<AbilityPlayer>().abilityMenuActive = false; }


        private void SwitchPassive(UIMouseEvent evt, UIElement listeningElement) {
            if (listeningElement == phantom)
            {
                _selectedPassiveID = ModContent.ProjectileType<BatProjectile>();
            }
            if (listeningElement == arrow)
            {
                _selectedPassiveID = ModContent.ProjectileType<HappyArrowPlusProjectileNoDrop>();
            }
        }

        public override void OnInitialize()
        {
            area = new UIElement();
            area.Top.Set(-25, 0f);
            area.Left.Set(64, 0f);
            area.Width.Set(800, 0f);
            area.Height.Set(800, 0f);
            area.HAlign = area.VAlign = 0.5f;

            menu = new UIImage(ModContent.Request<Texture2D>("OmoriMod/Systems/AbilitySystem/AbilityMenuUI/CrudeMenu"));
            menu.Left.Set(0, 0f);
            menu.Top.Set(0, 0f);
            menu.Width.Set(512, 0f);
            menu.Height.Set(512, 0f);


            confirm = new UIImageButton(ModContent.Request<Texture2D>("OmoriMod/Systems/AbilitySystem/AbilityMenuUI/CrudeConfirmIcon"));
            confirm.OnLeftClick += Confirm;
            confirm.Left.Set(0, .8f);
            confirm.Top.Set(0, .8f);
            confirm.Width.Set(64, 0f);
            confirm.Height.Set(64, 0f);

            cancel = new UIImageButton(ModContent.Request<Texture2D>("OmoriMod/Systems/AbilitySystem/AbilityMenuUI/CrudeExitIcon"));
            cancel.OnLeftClick += Cancel;
            cancel.Left.Set(0, 0.2f);
            cancel.Top.Set(0, .8f);
            cancel.Width.Set(64, 0f);
            cancel.Height.Set(64, 0f);


            arrow = new UIImageButton(ModContent.Request<Texture2D>("OmoriMod/Systems/AbilitySystem/AbilityMenuUI/SelectHappyArrow"));
            arrow.OnLeftClick += SwitchPassive;
            arrow.Left.Set(0, 0.2f);
            arrow.Top.Set(0, 0.2f);
            arrow.Width.Set(64, 0f);
            arrow.Height.Set(64, 0f);

            phantom = new UIImageButton(ModContent.Request<Texture2D>("OmoriMod/Systems/AbilitySystem/AbilityMenuUI/SelectRegular"));
            phantom.OnLeftClick += SwitchPassive;
            phantom.Left.Set(0, 0.5f);
            phantom.Top.Set(0, 0.2f);
            phantom.Width.Set(64, 0f);
            phantom.Height.Set(64, 0f);

            menu.Append(confirm);
            menu.Append(cancel);
            menu.Append(arrow);
            menu.Append(phantom);
            area.Append(menu);

            Append(area);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Main.LocalPlayer.GetModPlayer<AbilityPlayer>().abilityMenuActive)
            {
                return;
            }
            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Main.LocalPlayer.GetModPlayer<AbilityPlayer>().abilityMenuActive)
            {
                return;
            }
            base.Update(gameTime);
        }
    }
}
