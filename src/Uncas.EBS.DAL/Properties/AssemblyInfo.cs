using System;
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
    , MessageId = "EBS")]

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
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Uncas.EBS.DAL")]
[assembly: AssemblyCopyright("Copyright ©  2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("559cbf32-1fee-49e5-9a7b-f2c02927c596")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
