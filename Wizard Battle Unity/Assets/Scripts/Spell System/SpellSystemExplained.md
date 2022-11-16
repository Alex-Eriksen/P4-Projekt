# Spell System

## Classes

#### Spell _(BaseClass, NetworkBehaviour)_
> Spells like a fireball would inherit from this class to be functional.
> Requires a _SpellObject_ and a _VisualEffect_ component.

**Inherited Classes Examples:**
- FireballSpell
- WaterVortexSpell
- etc. 

Methods Missing Functionality:

	// Not all types and behaviours of spells are set yet.
	// They will be required to be added when it needs to.
	SetInitialTransform(NetworkIdentity identity) -> void

---

#### SpellObject _(DataClass, ScriptableObject)_
> Stores the data values that is unique to each spell, like mana cost, lifetime, etc.
> Created through the editor with:
> **"Right Click > Create > Spell System > _{Spell School}_ > New _{Spell School}_ "**

**Inherited Classes Examples:**
- UtilitySpellObject
- OffensiveSpellObject
- DefensiveSpellObject
- UltimateSpellObject

---

#### Status _(BaseClass, NetworkBehaviour)_
> Status effects like burn would inherit from this class to be functional.
> Requires a _StatusEffectObject_ and a _VisualEffect_ component.

**Inherited Classes Examples:**
- BurnStatus
- SlowStatus
- etc.

---

#### StatusEffectObject _(DataClass, ScriptableObject)_
> Stores the data values that is unique to each status effect, like its lifetime and whether it can be stacked or not.
> Created through the editor with:
**"Right Click > Create > Spell System > Status Effects > New Status Effect**

**Inherited Classes Examples:**
- N/A

---

## Creating A Spell Flow

###### 1. Create a directory for the spell:
	Assets/Resources/Spells/{SpellSchool}/{SubSpellSchool}/{SpellName}/{SpellTier}/

	Ex.: Assets/Resources/Spells/Elemental Spells/Fire Spells/Fireball/Tier I/

---

###### 2. Create 2 files in the newly created directory:
- The Visual Effects Graph object for the spells visual effects.
- The GameObject prefab that will be the spells GameObject.

---

###### 3. Create the C# script:
> Add the C# script in the folder that is the name of the spell, one step above the Tier folder.

	Ex.: Assets/Resources/Spells/Elemental Spells/Fire Spells/Fireball/FireballSpell.cs
Name the C# script after the spell name, ex.: 'FireballSpell.cs'

---

###### 4. Create the SpellObject:
The spell requires a SpellObject to configure the stats of the spell.
Its important to create the correct SpellObject for the type of spell you're making, ex.: Offensive Spell
The SpellObject should be placed in the folder with the tier of the spell,

	Ex.: Assets/Resources/Spells/Elemental Spells/Fire Spells/Fireball/Tier I/

The name of the SpellObject should contain a suffix with the spells' tier,
ex.:

	Fireball Tier_I
	Water Vortex Tier_I
> Note that we use roman numbering:

	I, II, III, IV, V

> For displaying the tier level and **not** 1, 2, 3, 4, 5.

---

###### 5. Configure the SpellObject:
Set its mana cost, lifetime, icon, etc.
Except for:

	Spell ID

The id will be automatically assigned in the next step.
Remember this can always be changed later on, no need to worry about balancing or anything when creating the spell.

---

###### 6. Add the SpellObject to the SpellDatabaseObject:
This will automatically assign an id to the SpellObject so that it has a unique id from the rest of the spells, without you having to manually make sure you're not using an id that is already used.
Drag the SpellObject into the array called 'Spell Objects' located in:

	Assets/Resources/Spells/

---

###### 7. Configuring the visual effect object:
The vfx object **must** contain 2 float properties that are exposed:

	Lifetime
	Size

If these are not added to the vfx object it will generate an error.

---

###### 8. Configuring the prefab:
The prefab **must** contain a Collider2D-, NetworkIdentity-, and a Visual Effect component.

Go to the Collider2D component and Check the "Is Trigger" box.
> Whether to check or uncheck this box is dependant on the type of spell you're creating, usually you want it to be checked.

Set the Visual Effect components "Sorting Layer" to either GroundVFX or VFX.
> The GroundVFX sorting layer will render the VFX underneath the entities like the player.

> The VFX sorting layer will render the VFX above the entities like the player.

Drag the visual effect object you created in step 2 into the "Asset Template" property on the Visual Effect component.

**Optionally** if the spell needs to move it will need a NetworkTransform- and Rigidbody2D component.

Some spells don't need to move themselves like the Teleport spell doesn't move itself, it moves the player that casts the spell instead.

Now you can add the C# script you created for the spell.
Assign the values it asks for by default:

	Spell Data: typeof(SpellObject)
	Vfx: typeof(VisualEffectComponent)

There is another values that must be set which is the "Contact Filter", press the property and Check the "Use Triggers" box.
> This will enable the collider system to only detect trigger colliders.

Lastly in the top right of the inspector set the "Layer" of the prefab to be on the "Spells" layer.

---

###### 9. Finishing Up:
The last step before you can start coding is to add the prefab of the spell to the NetworkManager.

This is a GameObject in the scene called "Network Manager".

Click on it and go to the "Wizard Network Manager" component script on the GameObject.

Find the "Registered Spawnable Prefabs" list, press the "+" icon on the bottom of it, and drag your new prefab into the slot that says:

	None (Game Object)

All thats left now is to code the C# script you created in step 3 to make the spell behave like you want to and make the VFX look good.

---

## Creating A Status Effect Flow

###### 1. Create a directory for the status effect:

	Assets/Resources/Spells/Status Effects/{StatusEffectName}/

	Ex.: Assets/Resources/Spells/Status Effects/Burn Status/

> Note that the folder **must** contains the suffix "Status" with a space as seperator.
If it doesn't it will generate an error.

---

###### 2. Create 3 files in the newly created directory:
- The Visual Effects Graph object for the spells visual effects.
- The C# script that will tell the status effect how to behave.
- The StatusEffectObject that configures the status effects.

---

###### 3. Create the prefab:
The prefab for the status effect **must** be within the folder:

	Assets/Resources/Spells/Status Effects
	
	Ex.: Assets/Resources/Spells/Status Effects/Burn.asset

---

###### 4. Configuring the StatusEffectObject:
Set the effect type to the type of status effect you're making.

Note that the EffectType is also the unique identifier for the status effect.
> Ex.: The "Burn Status" has the "Burn" effect type.
 

If the effect type for your status effect doesn't exists you must open the "StatusEffectType" script and add it to the enum.
Located in:

	Assets/Scripts/Spell System/Status Effects/

Set the lifetime of the status effect, I.E.: how long does the effect last for in seconds.

> The "Effect Stackable" and "Max Effect Stacks" are currently not implemented to do anything at the moment.

> You can still assign them if you wish to but they won't do anything.

The "Effect Icon" is the sprite that will be loaded on the UI when the status effect is applied to the target. 

---

###### 5. Configuring the Visual Effect Object:
The visual effect object **must** contain these exposed properties:

	Lifetime

If the visual effect object doesn't have them it will generate an error.

---

###### 6. Configuring the prefab:
The prefab **must** contain a NetworkIdentity-, and a Visual Effect component.

Set the Visual Effect components "Sorting Layer" to either GroundVFX or VFX.
> The GroundVFX sorting layer will render the VFX underneath the entities like the player.

> The VFX sorting layer will render the VFX above the entities like the player.

Assign the Visual Effect components "Asset Template" to the visual effect object you create in step 2.

Add your C# script to the prefab and assign the default values:

	Status Effect Data: typeof(StatusEffectObject)

Do **not** assign the "Opponent Network ID" property to anything. (defaults to 0)

---

###### 7. Finishing Up:
The last step before you can start coding is to add the prefab of the status effect to the NetworkManager.

This is a GameObject in the scene called "Network Manager".

Click on it and go to the "Wizard Network Manager" component script on the GameObject.

Find the "Registered Spawnable Prefabs" list, press the "+" icon on the bottom of it, and drag your new prefab into the slot that says:

	None (Game Object)

All thats left now is to code the C# script you created in step 2 to make the status effect behave like you want to and make the VFX look good.

---

## How To ...
<details>

<summary>Prefix Explinations...</summary>

> **Important**, the "SC" prefix means that the method can **only** be called from the server and on the server.
> > It **cannot** be called from the client or on the client.

> **Important**, the "CC" prefix means that the method can **only** be called from the client and on the client.
> > It **cannot** be called from the server or on the server.

> Note: the "Rpc" prefix means that the method will be called from the server and executed on all clients that are connected.

> Note: the "TRpc" prefix means that the method will be called from the server and executed on a specified client, see mirror documentation for a detailed explination. [Mirror Attributes](https://mirror-networking.gitbook.io/docs/guides/attributes)

> Note: the "Cmd" prefix means that the method will be called from the client and executed on the server.
> > **Important**, the client that executes the method **must** have authority over that NetworkIdentity in order to execute the method.

> Note: if a method does not have a prefix, it means the method will executed on whatever called it.
> > If the client calls the method, the client will execute it too.
>
> > If the server calls the method, the server will execute it too.

</details>

#### Add A Status Effects To An Entity:
You can call the 
	
	SC_AddStatusEffect(StatusEffect newStatusEffect) -> void

method on an entity add a status effect to it.

The method takes in a struct of a status effect which you can get from calling the

	GetStatusEffectStruct() -> StatusEffect

on your StatusEffectObject.

> Note: the Spell base class has a protected list of entities called "targetEntities" that is updated when the spell collides with an entity.

Thats all you need to do, the status effect will automatically expire after the set lifetime of the StatusEffectObject.

---

#### Add A Spell To The Spell Whell:
Locate the SpellbookObject in the

	Assets/Resources/Spells/

folder, click the SpellbookObject and assign the id of your SpellObject to one of the elements.

> Note: you must have added the SpellObject to the SpellDatabaseObject, in order to get the id of the SpellObject.

Thats all, you're done.

---

#### Manipulate An Entity's Health:
###### Gain Health
To regain the health of an entity you can call the method

	SC_GainHealth(float amount) -> void
on the entity you wish to regain health.

Which will add the specified amount of health to the entitty up to the maximum allowed for that entity.

<br></br>

###### Drain Health
To drain the health of an entity you can call the method

	SC_DrainHealth(float amount) -> void

on the entity you wish to drain health from.

This will remove the specified amount of health from the entity and check if the entity is destroyed after the removal of the health.

---

#### Manipulate An Entity's Mana:
###### Gain Mana
To regain the mana of an entity you can call the method

	SC_GainMana(float amount) -> void

Which will add the specified amount of mana to the entity up to the maximum allowed for that entity.

<br></br>

###### Drain Mana
To drain the mana of an entity you can call the method

	Cmd_DrainMana(float amount) -> void

on the entity you wish to drain mana from.

This will remove the specified amount of mana from the entity.

> Note: the PlayerCombat class automatically checks and drains mana when casting a spell.

---