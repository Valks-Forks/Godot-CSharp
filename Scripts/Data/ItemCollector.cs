﻿using System;
using Godot;
using RPG3D.General;
using RPG3D.General.Data;

public partial class ItemCollector: Node
{
    [Export] public Enums.ItemType ItemTyp = Enums.ItemType.Usable;
    [Export] public Enums.WeaponType WeaponType = Enums.WeaponType.NoType;
    [Export] public Enums.ArmorType ArmorType = Enums.ArmorType.NoType;
    [Export] public Enums.ShieldsType ShieldTyp = Enums.ShieldsType.NoType;
    [Export] public Enums.AccessoiresType AccessoiresType = Enums.AccessoiresType.NoType;
    [Export] public Enums.CraftType CraftType = Enums.CraftType.NoType;
    
    
    private DataManagement _dataManagment;

    public override void _Ready() {
        base._Ready();
        string path = GetDataPath();
        _dataManagment = GetParent<DataManagement>();
        var files = DirAccess.GetFilesAt(path);
        foreach (var file in files)
        {
            // var scene = ResourceLoader.Load(Path + file);
            var scene = GD.Load<PackedScene>(path + file);
            var loadedScene = scene.Instantiate();
            string realName = file.Left(file.Length - 5);
            loadedScene.Name = realName;
            AddChild(loadedScene);
            CharDataSimple converted = (CharDataSimple)loadedScene;
            _dataManagment.AddCharData(converted);
            loadedScene.QueueFree();
        }
    }

    private string GetDataPath() {
        string path = "res://Data/";
        if (ItemTyp == Enums.ItemType.Usable || ItemTyp == Enums.ItemType.Quest)
            path += ItemTyp.ToString();
        else if (ItemTyp == Enums.ItemType.Weapon) {
            if (WeaponType != Enums.WeaponType.NoType)
                path += WeaponType.ToString();
        }
        else if (ItemTyp == Enums.ItemType.Armor) {
            if (ArmorType != Enums.ArmorType.NoType)
                path += ArmorType.ToString();
        }
        else if (ItemTyp == Enums.ItemType.Shield) {
            if (ShieldTyp != Enums.ShieldsType.NoType)
                path += ShieldTyp.ToString();
        }
        else if (ItemTyp == Enums.ItemType.Accessoires) {
            if (AccessoiresType != Enums.AccessoiresType.NoType)
                path += AccessoiresType.ToString();
        }
        else if (ItemTyp == Enums.ItemType.Craft) {
            if (CraftType != Enums.CraftType.NoType)
                path += CraftType.ToString();
        }
        path += "/";
        return path;
    }
}