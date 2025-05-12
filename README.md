# Omori Mod
**Mod Author:** K3LV0N

This is a content expansion mod for Terraria based on Omori built using the tModLoader API. This README serves as a guide to understanding the mod's file structure and naming conventions.

## 📁 File Structure

The project is organized hierarchically for clarity and scalability, and is typically defined as such:

```
Items
├── Ammo
|	├── Arrows
|	|	├── Regular
|	|	└── Unlimited
|	└── Bullets
|	|	├── Regular
|	|	└── Unlimited
├── OtherItemType1
└── OtherItemType2
```

For a more general example:
```
InGameThing
├── ThingType1
|	├── Type1Specialization1
|	|	└── cs and png files
|	└── Type1Specialization2
└── ThingType2
```

## 🧾 Naming Conventions

To maintain clarity and consistency, the following naming conventions are used:

### Item Classes

- The class name typically matches the **in-game item name**.
  - Example: `HappyArrow.cs` defines the item named *Happy Arrow*.

### Projectiles

- Projectiles tied to a specific item use the format: ```[ItemName]Projectile[OptionalSpecialization]```

- For example: `HappyArrowProjectileNoDust.cs`
  - **'HappyArrow'**: Refers to the item it’s based on.
  - **'Projectile'**: Indicates it is a projectile.
  - **'NoDust'** *(optional)*: Extra details or behavior modifiers.

- This naming helps easily associate projectiles with their corresponding items.

### Buffs, NPCs, etc.

- Similar to items, class names generally reflect their in-game name for ease of reference.