using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;
using System;
using System.Collections.Generic;
using OmoriMod.Systems.EmotionSystem.Interfaces;

namespace OmoriMod.Items.Abstract_Classes
{
    /// <summary>
    /// An abstract class for items that inflict emotions.
    /// Use <see cref="AngryItem"/>, <see cref="HappyItem"/>, or <see cref="SadItem"/> 
    /// to set emotions. If <see cref="Emotion"/> is not set, it will default to <see cref="EmotionType.NONE"/>.
    /// </summary>
    public abstract class EmotionItem : ModItem, IOnHitEmotionObject
    {
        public EmotionType Emotion { get; protected set; }

        public float meleeWeaponProjectileMoveTime = 0.2f;

        /// <summary>
        /// Used to set the <see cref="Emotion"/>
        /// </summary>
        /// <param name="emotion">The emotion to be set.</param>
        protected void SetEmotionType(EmotionType emotion)
        {
            Emotion = emotion;
        }

        /// <summary>
        /// A hook method that allows emotion items to call <see cref="OnHitNPC(Player, NPC, NPC.HitInfo, int)"/> without breaking the emotion system.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="target">The target.</param>
        /// <param name="hit">The damage.</param>
        /// <param name="damageDone">The actual damage dealt to/taken by the NPC.</param>
        public virtual void OnHitNPCEmotion(Player player, NPC target, NPC.HitInfo hit, int damageDone) { }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            ((IOnHitEmotionObject)this).InflictEmotion(target);
            OnHitNPCEmotion(player, target, hit, damageDone);
        }

        public void SetAngryDefaults() { SetRarity(); }
        public void SetHappyDefaults() { SetRarity(); }
        public void SetSadDefaults() { SetRarity(); }

        private void SetRarity()
        {
            switch (Emotion)
            {
                case EmotionType.NONE:
                    break;
                case EmotionType.SAD:
                    Item.rare = ItemRarityID.Blue;
                    break;
                case EmotionType.ANGRY:
                    Item.rare = ItemRarityID.Red;
                    break;
                case EmotionType.HAPPY:
                    Item.rare = ItemRarityID.Yellow;
                    break;
            }
        }

        /// <summary>
        /// Sets the defaults that every item shares
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="scale"></param>
        /// <param name="buyPrice"></param>
        /// <param name="stackSize"></param>
        /// <param name="researchCount"></param>
        public void ItemDefaults(int width, int height, float scale, int buyPrice, int stackSize, int researchCount, bool consumable)
        {
            Item.scale = scale;
            Item.width = (int)(width * Item.scale);
            Item.height = (int)(height * Item.scale);
            Item.value = buyPrice;
            Item.maxStack = stackSize;
            Item.ResearchUnlockCount = researchCount;
            Item.consumable = consumable;
            SetRarity();
        }

        /// <summary>
        /// Sets the defaults for any item that does damage.
        /// </summary>
        /// <param name="damageType"></param>
        /// <param name="damage"></param>
        /// <param name="knockback"></param>
        /// <param name="crit"></param>
        /// <param name="noMelee"></param>
        public void DamageDefaults(DamageClass damageType, int damage, float knockback, int crit, bool noMelee, int mana = 0)
        {
            Item.damage = damage;
            Item.knockBack = knockback;
            Item.DamageType = damageType;
            Item.crit = crit;
            Item.noMelee = noMelee;
            Item.mana = mana;
        }

        /// <summary> 
        /// Sets the defaults for any item that has an animation.
        /// </summary>
        /// <param name="useTime"></param>
        /// <param name="useStyleID"></param>
        /// <param name="useSound"></param>
        /// <param name="autoReuse"></param>
        public void AnimationDefaults(int useTime, int useStyleID, SoundStyle useSound, bool autoReuse, int animationTime = -1, bool canTurnWhileUsing = false, bool noUseAnimation = false)
        {
            Item.useTime = useTime;
            if (animationTime == -1) { animationTime = useTime; }
            Item.useAnimation = animationTime;
            Item.useStyle = useStyleID;
            Item.UseSound = useSound;
            Item.autoReuse = autoReuse;
            Item.useTurn = canTurnWhileUsing;
            Item.noUseGraphic = noUseAnimation;
        }

        /// <summary>
        /// Sets the defaults for any item that shoots projectiles.
        /// </summary>
        /// <param name="ammoID"></param>
        /// <param name="projectileID"></param>
        /// <param name="shootSpeed"></param>
        public void ProjectileDefaults(int ammoID, int projectileID, float shootSpeed)
        {
            Item.ammo = ammoID;
            Item.shootSpeed = shootSpeed;
            Item.shoot = projectileID;
        }

        /// <summary>
        /// Sets the defaults for any consumable item.
        /// </summary>
        /// <param name="healthHealed"></param>
        /// <param name="manaHealed"></param>
        /// <param name="isPotion"></param>
        public void PotionDefaults(int healthHealed, int manaHealed, bool isPotion, int buffType = 0, int buffTimeInSeconds = 0)
        {
            Item.healLife = healthHealed;
            Item.healMana = manaHealed;
            Item.potion = isPotion;
            Item.buffType = buffType;
            Item.buffTime = buffTimeInSeconds * 60;
        }


        /// <summary>
        /// Special Method for setting Accessory Defaults
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="buyPrice"></param>
        public void SetAccessoryDefaults(int width, int height, int buyPrice)
        {
            Item.accessory = true;
            ItemDefaults(width: width, height: height, scale: 1, buyPrice: buyPrice, stackSize: 1, researchCount: 1, consumable: false);
        }

        /// <summary>
        /// Manually sets <see cref="Item.rare"/>. Must be called after <see cref="ItemDefaults(int, int, float, int, int, int, bool)."/>
        /// </summary>
        /// <param name="itemRarity">The rarity of the item.</param>
        public void SetItemRarity(int itemRarity)
        {
            Item.rare = itemRarity;
        }


        /// <summary>
        /// Clones the defaults of an <see cref="ModItem"/> inlcuding the research unlock count, 
        /// while preserving <see cref="Item.rare"/> and <see cref="Emotion"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Item"/> to be cloned.</typeparam>
        public void EmotionItemClone<T>() where T : ModItem
        {
            int itemType = ModContent.ItemType<T>();
            Item itemToClone = ModContent.GetModItem(itemType).Item;
            Item.CloneDefaults(itemType);
            Item.ResearchUnlockCount = itemToClone.ResearchUnlockCount;
            SetRarity();
            
        }

        /// <summary>
        /// Clones the defaults of a <see cref="ModItem"/> inlcuding the research unlock count, 
        /// while preserving <see cref="Item.rare"/> and <see cref="Emotion"/>. Changes projectile shot to
        /// the type of <paramref name="projType"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="ModItem"/> to be cloned.</typeparam>
        /// <param name="projType">The type of the <see cref="Projectile"/> shot.</param>
        public void EmotionItemCloneWithDifferentProjectile<T>(int newProjectileType) where T : ModItem
        {
            int itemType = ModContent.ItemType<T>();
            Item itemToClone = ModContent.GetModItem(itemType).Item;
            Item.CloneDefaults(itemType);
            Item.ResearchUnlockCount = itemToClone.ResearchUnlockCount;
            Item.shoot = newProjectileType;
            SetRarity();
        }

        /// <summary>
        /// Clones the defaults of a <see cref="ModItem"/> inlcuding the research unlock count, 
        /// while preserving <see cref="Item.rare"/> and <see cref="Emotion"/>. Changes buff applied  to
        /// the type of <paramref name="newBuffType"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="ModItem"/> to be cloned.</typeparam>
        /// <param name="projType">The type of the <see cref="Projectile"/> shot.</param>
        public void EmotionItemCloneWithDifferentBuff<T>(int newBuffType) where T : ModItem
        {
            int itemType = ModContent.ItemType<T>();
            Item itemToClone = ModContent.GetModItem(itemType).Item;
            Item.CloneDefaults(itemType);
            Item.ResearchUnlockCount = itemToClone.ResearchUnlockCount;
            Item.buffType = newBuffType;
            SetRarity();
        }

        /// <summary>
        /// Helps make ammo recipes.
        /// </summary>
        /// <param name="resultAmount">The amount of ammo being created from this recipe.</param>
        /// <param name="baseIngredientID">The ID of the ingredient that is used as the base for this item.</param>
        /// <param name="nonEndlessIngredientID">The ID of the ingredient used for non-endless crafting.</param>
        /// <param name="endlessIngredientID">The ID of the ingredient used for endless crafting.</param>
        /// <param name="baseAmount">The amount of the base ingredient needed.</param>
        /// <param name="nonEndlessAmount">The amount of the non-endless ingredient needed.</param>
        public void MakeAmmoRecipes(int resultAmount, int baseIngredientID, int nonEndlessIngredientID, int endlessIngredientID, int baseAmount = 1, int nonEndlessAmount = 1)
        {
            Recipe recipe1 = CreateRecipe(resultAmount);
            recipe1.AddIngredient(baseIngredientID, baseAmount);
            recipe1.AddIngredient(nonEndlessIngredientID, nonEndlessAmount);
            recipe1.Register();

            Recipe recipe2 = CreateRecipe(resultAmount);
            recipe2.AddIngredient(baseIngredientID, baseAmount);
            recipe2.AddCondition(Condition.PlayerCarriesItem(endlessIngredientID));
            recipe2.Register();
        }

        /// <summary>
        /// Helps make endless ammo recipes.
        /// </summary>
        /// <param name="ingredientID">The non-endless ammo varient ID.</param>
        /// <param name="ammoNeeded">The amount of non-endless ammo needed.</param>
        /// <param name="craftingStationID">The ID of the crafting station used.</param>
        public void MakeEndlessAmmoRecipe(int ingredientID, int ammoNeeded = 3996, int craftingStationID = TileID.CrystalBall)
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ingredientID, ammoNeeded);
            recipe1.AddTile(craftingStationID);
            recipe1.Register();
        }
        /// <summary>
        /// Makes regular 1-ingredient recipes
        /// </summary>
        /// <param name="ingredientID">The ingredient's ID.</param>
        /// <param name="amount">The amount of the ingredient needed.</param>
        /// <param name="craftingStationID">The ID of the crafting station used.</param>
        public void MakeRegularRecipe(int ingredientID, int amount, int craftingStationID)
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ingredientID, amount);
            recipe.AddTile(craftingStationID);
            recipe.Register();
        }

        /// <summary>
        /// Makes a bunch of 1-ingredient recipes
        /// </summary>
        /// <param name="ingredients">A list where you have each value be (ingredientID, amount).</param>
        /// <param name="craftingStationID">The ID of the crafting station used.</param>
        public void MakeRegularRecipes(List<(int, int)> ingredients, int craftingStationID)
        {
            foreach(var ingredient in ingredients)
            {
                int ing = ingredient.Item1;
                int amount = ingredient.Item2;
                MakeRegularRecipe(ing, amount, craftingStationID);
            }
        }

        /// <summary>
        /// Makes an 'upgrade' recipe
        /// </summary>
        /// <param name="baseItemID">The item to be 'upgraded'.</param>
        /// <param name="extraItemID">The ID of the upgrade material.</param>
        /// <param name="extraItemAmount">The amount of upgrade material needed.</param>
        /// <param name="craftingStationID">The ID of the crafting station used.</param>
        public void MakeUpgradeRecipe(int baseItemID, int extraItemID, int extraItemAmount, int craftingStationID)
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(baseItemID, 1);
            recipe.AddIngredient(extraItemID, extraItemAmount);
            recipe.AddTile(craftingStationID);
            recipe.Register();
        }

        /// <summary>
        /// Moves projectile forward for spawning purposes.
        /// </summary>
        /// <param name="position">The current position of the projectile.</param>
        /// <param name="velocity">The current velocity of the projectile.</param>
        /// <param name="ticks">How many ticks to simulate this projectile moving for.</param>
        /// <returns><c>True</c> if no collision occured.</returns>
        public virtual bool MoveProjectileForward(ref Vector2 position, ref Vector2 velocity, float ticks = 2)
        {   
            Projectile projectile = ModContent.GetModProjectile(Item.shoot).Projectile;

            int actingTicks = (int)MathF.Floor(ticks);
            Vector2 actingVelocity = velocity;
            
            while (ticks % 1 != 0)
            {
                actingTicks *= 10;
                ticks *= 10;
                actingVelocity /= 10;
            }

            for(int i = 0; i < actingTicks; i++)
            {
                // compute next canidate position
                Vector2 nextPos = position + actingVelocity;
                Rectangle hitbox = new Rectangle(
                    (int)nextPos.X,
                    (int)nextPos.Y,
                    projectile.width,
                    projectile.height
                );

                // If that spot collides with solid tiles, abort early
                if (Collision.SolidCollision(hitbox.TopLeft(), hitbox.Width, hitbox.Height))
                {
                    return false;
                }

                position = nextPos;
            }

            return true;
        }
    }
}
