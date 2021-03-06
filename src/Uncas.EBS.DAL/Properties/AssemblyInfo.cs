﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

[module: SuppressMessage("Microsoft.Naming"
    , "CA1709:IdentifiersShouldBeCasedCorrectly"
    , Scope = "type"
    , Target = "Uncas.EBS.DAL.EBSDataContext"
    , MessageId = "EBS")]

[module: SuppressMessage("Microsoft.Naming"
    , "CA1709:IdentifiersShouldBeCasedCorrectly"
    , MessageId = "DAL")]

[module: SuppressMessage("Microsoft.Naming"
    , "CA1709:IdentifiersShouldBeCasedCorrectly"
    , Scope = "namespace"
    , Target = "Uncas.EBS.DAL"
    , MessageId = "DAL")]

[module: SuppressMessage("Microsoft.Naming"
    , "CA1709:IdentifiersShouldBeCasedCorrectly"
    , Scope = "namespace"
    , Target = "Uncas.EBS.DAL"
    , MessageId = "EBS")]

[module: SuppressMessage("Microsoft.Usage"
    , "CA2227:CollectionPropertiesShouldBeReadOnly"
    , Scope = "member"
    , Target = "Uncas.EBS.DAL.Issue.#Tasks")]

[module: SuppressMessage("Microsoft.Usage"
    , "CA2227:CollectionPropertiesShouldBeReadOnly"
    , Scope = "member"
    , Target = "Uncas.EBS.DAL.Person.#PersonOffs")]

[module: SuppressMessage("Microsoft.Usage"
    , "CA2227:CollectionPropertiesShouldBeReadOnly"
    , Scope = "member"
    , Target = "Uncas.EBS.DAL.Person.#Tasks")]

[module: SuppressMessage("Microsoft.Usage"
    , "CA2227:CollectionPropertiesShouldBeReadOnly"
    , Scope = "member"
    , Target = "Uncas.EBS.DAL.Project.#Issues")]


[module: SuppressMessage("Microsoft.Usage"
    , "CA2227:CollectionPropertiesShouldBeReadOnly"
    , Scope = "member"
    , Target = "Uncas.EBS.DAL.Status.#Issues")]


[module: SuppressMessage("Microsoft.Usage"
    , "CA2227:CollectionPropertiesShouldBeReadOnly"
    , Scope = "member"
    , Target = "Uncas.EBS.DAL.Status.#Tasks")]

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Uncas.EBS.DAL")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("559cbf32-1fee-49e5-9a7b-f2c02927c596")]