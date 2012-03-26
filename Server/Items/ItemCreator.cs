namespace MMO3D.Server
{
    using MMO3D.CommonCode;

    /// <summary>
    /// Class used to create instances of items.
    /// </summary>
    public static class ItemCreator
    {
        /// <summary>
        /// Creates an item class from an instance.
        /// </summary>
        /// <param name="itemInstance">The ID of the item to create.</param>
        /// <returns>Returns an instance of the correct class to hold this item.</returns>
        /// <exception cref="UndefinedItemException">Thrown if the item was not found in the database.</exception>
        public static Item CreateFromId(long itemInstance)
        {
            throw new UndefinedItemException(-1);
            /*long itemId = 0; // itemInstance.ItemId.FieldValue.Value;

            ItemClass itemClass = ItemClass.Undefined;
            string itemName = null;
            string itemDescription = null;
            byte itemEvasionBonus = 0;
            short itemIntegrity = 0;
            byte itemIntegrityLevel = 0;
            short itemMaximumIntegrity = 0;
            float itemWeight = 0;
            byte itemAttackBonus = 0;
            int itemAttackSpeed = 0;
            WeaponType1H item1HWeaponType = WeaponType1H.Undefined;
            WeaponType2H item2HWeaponType = WeaponType2H.Undefined;
            ArmorType itemArmorType = ArmorType.Undefined;
            byte itemDefenseBonus = 0;
            byte itemCastersPouches = 0;
            byte itemPotionStraps = 0;
            byte itemToolPouches = 0;
            byte itemWeaponSheaths = 0;
            byte itemMaxCharges = 0;

            var theItem = (from p in GameServer.Instance.DbManager.Items where p.ID = itemId select p).SingleOrDefault();
            if (theItem == null)
            {
                throw new UndefinedItemException(itemId);
            }
            else
            {
                // Base data
                itemClass = theItem.Class.FieldValue.Value;
                itemName = theItem.Name.FieldValue;
                itemDescription = theItem.Description.FieldValue;

                // Munitions
                itemIntegrity = theItem.MunitionData.Integrity.FieldValue.Value;
                itemIntegrityLevel = theItem.MunitionData.IntegrityLevel.FieldValue.Value;
                itemMaximumIntegrity = theItem.MunitionData.MaximumIntegrity.FieldValue.Value;
                itemWeight = theItem.MunitionData.Weight.FieldValue.Value;

                // Armor doesn't support evasion bonus and will throw
                // an exception if it is set
                itemEvasionBonus = theItem.MunitionData.EvasionBonus.FieldValue.Value;

                // Weapons
                itemAttackBonus = theItem.OffensiveMunitionData.AttackBonus.FieldValue.Value;
                itemAttackSpeed = theItem.OffensiveMunitionData.AttackSpeed.FieldValue.Value;

                // One handed weapon...
                item1HWeaponType = theItem.OffensiveMunitionData.WeaponType1.FieldValue.Value;

                // Two handed weapon...
                item2HWeaponType = theItem.OffensiveMunitionData.WeaponType2.FieldValue.Value;

                // Defensive munitions
                itemArmorType = theItem.DefensiveMunitionData.ArmorsType.FieldValue.Value;
                itemDefenseBonus = theItem.DefensiveMunitionData.DefenseBonus.FieldValue.Value;

                // Belts
                itemCastersPouches = theItem.BeltData.CastersPouches.FieldValue.Value;
                itemPotionStraps = theItem.BeltData.PotionStraps.FieldValue.Value;
                itemToolPouches = theItem.BeltData.ToolPouches.FieldValue.Value;
                itemWeaponSheaths = theItem.BeltData.WeaponSheaths.FieldValue.Value;

                // Spell shards
                itemMaxCharges = theItem.SpellShardData.MaxCharges.FieldValue.Value;
            }

            Item returnedItem = null;

            switch (itemClass)
            {
                case ItemClass.Accessory:
                    returnedItem = new Accessory(itemId, itemName, itemDescription);
                    break;
                case ItemClass.Armor:
                    returnedItem = new Armor(itemId, itemName, itemDescription, itemWeight, itemIntegrity, itemMaximumIntegrity, itemIntegrityLevel, itemEvasionBonus, itemArmorType, itemDefenseBonus);
                    break;
                case ItemClass.Belt:
                    returnedItem = new Belt(itemId, itemName, itemDescription, itemPotionStraps, itemWeaponSheaths, itemCastersPouches, itemToolPouches);
                    break;
                case ItemClass.Food:
                    returnedItem = new Food(itemId, itemName, itemDescription);
                    break;
                case ItemClass.Material:
                    returnedItem = new Material(itemId, itemName, itemDescription);
                    break;
                case ItemClass.MiscellaneousItem:
                    returnedItem = new MiscellaneousItem(itemId, itemName, itemDescription);
                    break;
                case ItemClass.OneHandedWeapon:
                    returnedItem = new OneHandedWeapon(itemId, itemName, itemDescription, itemWeight, itemIntegrity, itemMaximumIntegrity, itemIntegrityLevel, itemEvasionBonus, itemAttackBonus, itemAttackSpeed, item1HWeaponType);
                    break;
                case ItemClass.Potion:
                    returnedItem = new Potion(itemId, itemName, itemDescription);
                    break;
                case ItemClass.Shield:
                    returnedItem = new Shield(itemId, itemName, itemDescription, itemWeight, itemIntegrity, itemMaximumIntegrity, itemIntegrityLevel, itemEvasionBonus, itemArmorType, itemDefenseBonus);
                    break;
                case ItemClass.SpellShard:
                    returnedItem = new SpellShard(itemId, itemName, itemDescription, itemMaxCharges);
                    break;
                case ItemClass.Tool:
                    returnedItem = new Tool(itemId, itemName, itemDescription);
                    break;
                case ItemClass.TwoHandedWeapon:
                    returnedItem = new TwoHandedWeapon(itemId, itemName, itemDescription, itemWeight, itemIntegrity, itemMaximumIntegrity, itemIntegrityLevel, itemEvasionBonus, itemAttackBonus, itemAttackSpeed, item2HWeaponType);
                    break;
                default:
                    throw new UndefinedItemException(itemId);
            }

            return returnedItem;*/
        }
    }
}
