﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5448
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("config", Namespace="", IsNullable=false)]
public partial class moduleConfiguration {
    
    private moduleTitle moduleNameField;
    
    private headerImage moduleImageField;
    
    private compositeDependency moduleDependenciesField;
    
    private fileList requiredInstallFilesField;
    
    private stepList installStepsField;
    
    private conditionalFileInstallList conditionalFileInstallsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public moduleTitle moduleName {
        get {
            return this.moduleNameField;
        }
        set {
            this.moduleNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public headerImage moduleImage {
        get {
            return this.moduleImageField;
        }
        set {
            this.moduleImageField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public compositeDependency moduleDependencies {
        get {
            return this.moduleDependenciesField;
        }
        set {
            this.moduleDependenciesField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public fileList requiredInstallFiles {
        get {
            return this.requiredInstallFilesField;
        }
        set {
            this.requiredInstallFilesField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public stepList installSteps {
        get {
            return this.installStepsField;
        }
        set {
            this.installStepsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public conditionalFileInstallList conditionalFileInstalls {
        get {
            return this.conditionalFileInstallsField;
        }
        set {
            this.conditionalFileInstallsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class moduleTitle {
    
    private moduleTitlePosition positionField;
    
    private byte[] colourField;
    
    private string valueField;
    
    public moduleTitle() {
        this.positionField = moduleTitlePosition.Left;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(moduleTitlePosition.Left)]
    public moduleTitlePosition position {
        get {
            return this.positionField;
        }
        set {
            this.positionField = value;
        }
    }
    
    /// <remarks/>
    // CODEGEN Warning: DefaultValue attribute on members of type System.Byte[] is not supported in this version of the .Net Framework.
    // CODEGEN Warning: 'default' attribute on items of type 'hexBinary' is not supported in this version of the .Net Framework.  Ignoring default='000000' attribute.
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="hexBinary")]
    public byte[] colour {
        get {
            return this.colourField;
        }
        set {
            this.colourField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public enum moduleTitlePosition {
    
    /// <remarks/>
    Left,
    
    /// <remarks/>
    Right,
    
    /// <remarks/>
    RightOfImage,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class conditionalInstallPattern {
    
    private compositeDependency dependenciesField;
    
    private fileList filesField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public compositeDependency dependencies {
        get {
            return this.dependenciesField;
        }
        set {
            this.dependenciesField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public fileList files {
        get {
            return this.filesField;
        }
        set {
            this.filesField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class compositeDependency {
    
    private object[] itemsField;
    
    private ItemsChoiceType[] itemsElementNameField;
    
    private compositeDependencyOperator operatorField;
    
    public compositeDependency() {
        this.operatorField = compositeDependencyOperator.And;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("dependencies", typeof(compositeDependency), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("fileDependency", typeof(fileDependency), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("flagDependency", typeof(flagDependency), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("fommDependency", typeof(versionDependency), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("gameDependency", typeof(versionDependency), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public object[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType[] ItemsElementName {
        get {
            return this.itemsElementNameField;
        }
        set {
            this.itemsElementNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(compositeDependencyOperator.And)]
    public compositeDependencyOperator @operator {
        get {
            return this.operatorField;
        }
        set {
            this.operatorField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class fileDependency {
    
    private string fileField;
    
    private fileDependencyState stateField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string file {
        get {
            return this.fileField;
        }
        set {
            this.fileField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public fileDependencyState state {
        get {
            return this.stateField;
        }
        set {
            this.stateField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public enum fileDependencyState {
    
    /// <remarks/>
    Missing,
    
    /// <remarks/>
    Inactive,
    
    /// <remarks/>
    Active,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class flagDependency {
    
    private string flagField;
    
    private string valueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string flag {
        get {
            return this.flagField;
        }
        set {
            this.flagField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class versionDependency {
    
    private string versionField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string version {
        get {
            return this.versionField;
        }
        set {
            this.versionField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema=false)]
public enum ItemsChoiceType {
    
    /// <remarks/>
    dependencies,
    
    /// <remarks/>
    fileDependency,
    
    /// <remarks/>
    flagDependency,
    
    /// <remarks/>
    fommDependency,
    
    /// <remarks/>
    gameDependency,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public enum compositeDependencyOperator {
    
    /// <remarks/>
    And,
    
    /// <remarks/>
    Or,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class fileList {
    
    private fileSystemItem[] itemsField;
    
    private ItemsChoiceType1[] itemsElementNameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("file", typeof(fileSystemItem), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("folder", typeof(fileSystemItem), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public fileSystemItem[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType1[] ItemsElementName {
        get {
            return this.itemsElementNameField;
        }
        set {
            this.itemsElementNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class fileSystemItem {
    
    private string sourceField;
    
    private string destinationField;
    
    private bool alwaysInstallField;
    
    private bool installIfUsableField;
    
    private string priorityField;
    
    public fileSystemItem() {
        this.alwaysInstallField = false;
        this.installIfUsableField = false;
        this.priorityField = "0";
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string source {
        get {
            return this.sourceField;
        }
        set {
            this.sourceField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string destination {
        get {
            return this.destinationField;
        }
        set {
            this.destinationField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(false)]
    public bool alwaysInstall {
        get {
            return this.alwaysInstallField;
        }
        set {
            this.alwaysInstallField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(false)]
    public bool installIfUsable {
        get {
            return this.installIfUsableField;
        }
        set {
            this.installIfUsableField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
    [System.ComponentModel.DefaultValueAttribute("0")]
    public string priority {
        get {
            return this.priorityField;
        }
        set {
            this.priorityField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema=false)]
public enum ItemsChoiceType1 {
    
    /// <remarks/>
    file,
    
    /// <remarks/>
    folder,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class conditionalFileInstallList {
    
    private conditionalInstallPattern[] patternsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("pattern", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
    public conditionalInstallPattern[] patterns {
        get {
            return this.patternsField;
        }
        set {
            this.patternsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class dependencyPattern {
    
    private compositeDependency dependenciesField;
    
    private pluginType typeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public compositeDependency dependencies {
        get {
            return this.dependenciesField;
        }
        set {
            this.dependenciesField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public pluginType type {
        get {
            return this.typeField;
        }
        set {
            this.typeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class pluginType {
    
    private pluginTypeEnum nameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public pluginTypeEnum name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
public enum pluginTypeEnum {
    
    /// <remarks/>
    Required,
    
    /// <remarks/>
    Optional,
    
    /// <remarks/>
    Recommended,
    
    /// <remarks/>
    NotUsable,
    
    /// <remarks/>
    CouldBeUsable,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class dependencyPluginType {
    
    private pluginType defaultTypeField;
    
    private dependencyPattern[] patternsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public pluginType defaultType {
        get {
            return this.defaultTypeField;
        }
        set {
            this.defaultTypeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("pattern", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
    public dependencyPattern[] patterns {
        get {
            return this.patternsField;
        }
        set {
            this.patternsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class pluginTypeDescriptor {
    
    private object itemField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("dependencyType", typeof(dependencyPluginType), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("type", typeof(pluginType), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public object Item {
        get {
            return this.itemField;
        }
        set {
            this.itemField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class conditionFlagList {
    
    private setConditionFlag[] flagField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("flag", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public setConditionFlag[] flag {
        get {
            return this.flagField;
        }
        set {
            this.flagField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class setConditionFlag {
    
    private string nameField;
    
    private string valueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class image {
    
    private string pathField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string path {
        get {
            return this.pathField;
        }
        set {
            this.pathField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class plugin {
    
    private string descriptionField;
    
    private image imageField;
    
    private object[] itemsField;
    
    private pluginTypeDescriptor typeDescriptorField;
    
    private string nameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string description {
        get {
            return this.descriptionField;
        }
        set {
            this.descriptionField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public image image {
        get {
            return this.imageField;
        }
        set {
            this.imageField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("conditionFlags", typeof(conditionFlagList), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("files", typeof(fileList), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public object[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public pluginTypeDescriptor typeDescriptor {
        get {
            return this.typeDescriptorField;
        }
        set {
            this.typeDescriptorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class pluginList {
    
    private plugin[] pluginField;
    
    private orderEnum orderField;
    
    public pluginList() {
        this.orderField = orderEnum.Ascending;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("plugin", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public plugin[] plugin {
        get {
            return this.pluginField;
        }
        set {
            this.pluginField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(orderEnum.Ascending)]
    public orderEnum order {
        get {
            return this.orderField;
        }
        set {
            this.orderField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
public enum orderEnum {
    
    /// <remarks/>
    Ascending,
    
    /// <remarks/>
    Descending,
    
    /// <remarks/>
    Explicit,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class group {
    
    private pluginList pluginsField;
    
    private string nameField;
    
    private groupType typeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public pluginList plugins {
        get {
            return this.pluginsField;
        }
        set {
            this.pluginsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public groupType type {
        get {
            return this.typeField;
        }
        set {
            this.typeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public enum groupType {
    
    /// <remarks/>
    SelectAtLeastOne,
    
    /// <remarks/>
    SelectAtMostOne,
    
    /// <remarks/>
    SelectExactlyOne,
    
    /// <remarks/>
    SelectAll,
    
    /// <remarks/>
    SelectAny,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class groupList {
    
    private group[] groupField;
    
    private orderEnum orderField;
    
    public groupList() {
        this.orderField = orderEnum.Ascending;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("group", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public group[] group {
        get {
            return this.groupField;
        }
        set {
            this.groupField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(orderEnum.Ascending)]
    public orderEnum order {
        get {
            return this.orderField;
        }
        set {
            this.orderField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class installStep {
    
    private compositeDependency visibleField;
    
    private groupList optionalFileGroupsField;
    
    private string nameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public compositeDependency visible {
        get {
            return this.visibleField;
        }
        set {
            this.visibleField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public groupList optionalFileGroups {
        get {
            return this.optionalFileGroupsField;
        }
        set {
            this.optionalFileGroupsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class stepList {
    
    private installStep[] installStepField;
    
    private orderEnum orderField;
    
    public stepList() {
        this.orderField = orderEnum.Ascending;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("installStep", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public installStep[] installStep {
        get {
            return this.installStepField;
        }
        set {
            this.installStepField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(orderEnum.Ascending)]
    public orderEnum order {
        get {
            return this.orderField;
        }
        set {
            this.orderField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class headerImage {
    
    private string pathField;
    
    private bool showImageField;
    
    private bool showFadeField;
    
    private int heightField;
    
    public headerImage() {
        this.showImageField = true;
        this.showFadeField = true;
        this.heightField = -1;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string path {
        get {
            return this.pathField;
        }
        set {
            this.pathField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(true)]
    public bool showImage {
        get {
            return this.showImageField;
        }
        set {
            this.showImageField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(true)]
    public bool showFade {
        get {
            return this.showFadeField;
        }
        set {
            this.showFadeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(-1)]
    public int height {
        get {
            return this.heightField;
        }
        set {
            this.heightField = value;
        }
    }
}
