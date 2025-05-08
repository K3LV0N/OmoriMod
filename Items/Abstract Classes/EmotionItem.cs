using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;

namespace OmoriMod.Items.Abstract_Classes
{
    /// <summary>
    /// An abstract class for items that inflict emotions.
    /// Use <see cref="AngryItem"/>, <see cref="HappyItem"/>, or <see cref="SadItem"/> 
    /// to set emotions. If <see cref="Emotion"/> is not set, it will default to <see cref="EmotionType.NONE"/>.
    /// </summary>
    public abstract class EmotionItem : ModItem, IEmotionObject
    {
        public EmotionType Emotion { get; set; }

        /// <summary>
        /// Useful for when you need to manually set the emotion type
        /// </summary>
        /// <param name="emotion"></param>
        protected void SetEmotionType(EmotionType emotion)
        {
            Emotion = emotion;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            ((IEmotionObject)this).InflictEmotion(target);
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

        private void ItemDefaults(int width, int height, float scale, int buyPrice, int stackSize, int researchCount)
        {
            Item.scale = scale;
            Item.width = (int)(width * Item.scale);
            Item.height = (int)(height * Item.scale);
            Item.value = buyPrice;
            Item.maxStack = stackSize;
            Item.ResearchUnlockCount = researchCount;
            SetRarity();
            
        }

        private void DamageDefaults(DamageClass damageType, int damage, float knockback)
        {
            Item.damage = damage;
            Item.knockBack = knockback;
            Item.DamageType = damageType;
        }

        private void AnimationDefaults(int useTime, int useStyleID, SoundStyle useSound, bool autoReuse)
        {
            Item.useTime = useTime;
            Item.useAnimation = Item.useTime;
            Item.useStyle = useStyleID;
            Item.UseSound = useSound;
            Item.autoReuse = autoReuse;
        }

        public void SetAccessoryDefaults(int width, int height, int buyPrice)
        {
            Item.accessory = true;
            ItemDefaults(width: width, height: height, scale: 1, buyPrice: buyPrice, stackSize: 1, researchCount: 1);
        }

        public void SetMeleeWeaponDefaults(int width, int height, int buyPrice, int damage, float knockback, int useTime, int useStyleID, SoundStyle useSound, float scale = 1, bool autoReuse = false)
        {
            DamageClass damageType = DamageClass.Melee;
            DamageDefaults(damageType, damage, knockback);
            AnimationDefaults(useTime, useStyleID, useSound, autoReuse);
            ItemDefaults(width: width, height: height, scale: scale, buyPrice: buyPrice, stackSize: 1, researchCount: 1);
        }

        /// <summary>
        /// Variant of Melee Weapon Defaults that allows a projectile to be specified for the weapon to shoot
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="buyPrice"></param>
        /// <param name="damage"></param>
        /// <param name="knockback"></param>
        /// <param name="useTime"></param>
        /// <param name="useStyleID"></param>
        /// <param name="useSound"></param>
        /// <param name="scale"></param>
        /// <param name="autoReuse"></param>
        public void SetMeleeWeaponWithProjectileDefaults<T>(int width, int height, int buyPrice, int damage, float knockback, int useTime, int useStyleID, SoundStyle useSound, float shootSpeed, float scale = 1, bool autoReuse = false) where T : ModProjectile
        {
            DamageClass damageType = DamageClass.Melee;
            DamageDefaults(damageType, damage, knockback);
            AnimationDefaults(useTime, useStyleID, useSound, autoReuse);
            ItemDefaults(width: width, height: height, scale: scale, buyPrice: buyPrice, stackSize: 1, researchCount: 1);
            Item.shoot = ModContent.ProjectileType<T>();
            Item.shootSpeed = shootSpeed;
        }

        public void SetMagicWeaponWithProjectileDefaults<T>(int width, int height, int buyPrice, int damage, float knockback, int useTime, int useStyleID, SoundStyle useSound, float shootSpeed, int mana, float scale = 1, bool autoReuse = false) where T : ModProjectile
        {
            DamageClass damageType = DamageClass.Magic;
            DamageDefaults(damageType, damage, knockback);
            AnimationDefaults(useTime, useStyleID, useSound, autoReuse);
            ItemDefaults(width: width, height: height, scale: scale, buyPrice: buyPrice, stackSize: 1, researchCount: 1);
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<T>();
            Item.shootSpeed = shootSpeed;
            Item.mana = mana;
        }

        /// <summary>
        /// Clones the defaults of an item inlcuding the research unlock count
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void EmotionItemClone<T>() where T : ModItem
        {
            
            int itemType = ModContent.ItemType<T>();
            Item itemToClone = ModContent.GetModItem(itemType).Item;
            Item.CloneDefaults(itemType);
            Item.ResearchUnlockCount = itemToClone.ResearchUnlockCount;
            SetRarity();
            
        }

        /// <summary>
        /// Clones the defaults of an item inlcuding the research unlock count. This also allows the changing of the projectile shot from an item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void EmotionItemCloneWithDifferentProjectile<T>(int projType) where T : ModItem
        {

            int itemType = ModContent.ItemType<T>();
            Item itemToClone = ModContent.GetModItem(itemType).Item;
            Item.CloneDefaults(itemType);
            Item.ResearchUnlockCount = itemToClone.ResearchUnlockCount;
            Item.shoot = projType;
            SetRarity();

        }

        public float meleeWeaponProjectileMoveTime = 0.2f;

        /// <summary>
        /// <c>MoveProjectileForward</c> moves a projectile shot by a weapon forward slightly to reduce spawn collisions. <br />
        /// <paramref name="position"/> is current position of the projectile. This <c>WILL</c>c be changed here.<br />
        /// <paramref name="velocity"/> is the current velocity of the projectile.<br />
        /// <paramref name="ticks"/> is the amount of ticks to move the projectile forward. Float for increased precision.<br />
        /// </summary>
        public virtual void MoveProjectileForward(ref Vector2 position, ref Vector2 velocity, float ticks = 2.1f)
        {   
            position = position + (velocity * ticks);
        }
    }
}
