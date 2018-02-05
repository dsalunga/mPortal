﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("Model", "FK_States_Countries", "Country", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(WCMS.WebSystem.Apps.Integration.ExternalCountry), "State", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(WCMS.WebSystem.Apps.Integration.State), true)]

#endregion

namespace WCMS.WebSystem.Apps.Integration
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class ExternalDBEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new ExternalDBEntities object using the connection string found in the 'ExternalDBEntities' section of the application configuration file.
        /// </summary>
        public ExternalDBEntities() : base("name=ExternalDBEntities", "ExternalDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ExternalDBEntities object.
        /// </summary>
        public ExternalDBEntities(string connectionString) : base(connectionString, "ExternalDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ExternalDBEntities object.
        /// </summary>
        public ExternalDBEntities(EntityConnection connection) : base(connection, "ExternalDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<ExternalCountry> ExtCountries
        {
            get
            {
                if ((_ExtCountries == null))
                {
                    _ExtCountries = base.CreateObjectSet<ExternalCountry>("ExtCountries");
                }
                return _ExtCountries;
            }
        }
        private ObjectSet<ExternalCountry> _ExtCountries;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<State> States
        {
            get
            {
                if ((_States == null))
                {
                    _States = base.CreateObjectSet<State>("States");
                }
                return _States;
            }
        }
        private ObjectSet<State> _States;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the AMSCountries EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAMSCountries(ExternalCountry aMSCountry)
        {
            base.AddObject("ExtCountries", aMSCountry);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the States EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToStates(State state)
        {
            base.AddObject("States", state);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="ExternalCountry")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class ExternalCountry : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new ExternalCountry object.
        /// </summary>
        /// <param name="countryID">Initial value of the CountryID property.</param>
        /// <param name="dateCreated">Initial value of the DateCreated property.</param>
        /// <param name="createdBy">Initial value of the CreatedBy property.</param>
        /// <param name="dateUpdated">Initial value of the DateUpdated property.</param>
        /// <param name="updatedBy">Initial value of the UpdatedBy property.</param>
        public static ExternalCountry CreateExtCountry(global::System.Int16 countryID, global::System.DateTime dateCreated, global::System.Int32 createdBy, global::System.DateTime dateUpdated, global::System.Int32 updatedBy)
        {
            ExternalCountry extCountry = new ExternalCountry();
            extCountry.CountryID = countryID;
            extCountry.DateCreated = dateCreated;
            extCountry.CreatedBy = createdBy;
            extCountry.DateUpdated = dateUpdated;
            extCountry.UpdatedBy = updatedBy;
            return extCountry;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int16 CountryID
        {
            get
            {
                return _CountryID;
            }
            set
            {
                if (_CountryID != value)
                {
                    OnCountryIDChanging(value);
                    ReportPropertyChanging("CountryID");
                    _CountryID = StructuralObject.SetValidValue(value, "CountryID");
                    ReportPropertyChanged("CountryID");
                    OnCountryIDChanged();
                }
            }
        }
        private global::System.Int16 _CountryID;
        partial void OnCountryIDChanging(global::System.Int16 value);
        partial void OnCountryIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> RegionID
        {
            get
            {
                return _RegionID;
            }
            set
            {
                OnRegionIDChanging(value);
                ReportPropertyChanging("RegionID");
                _RegionID = StructuralObject.SetValidValue(value, "RegionID");
                ReportPropertyChanged("RegionID");
                OnRegionIDChanged();
            }
        }
        private Nullable<global::System.Int16> _RegionID;
        partial void OnRegionIDChanging(Nullable<global::System.Int16> value);
        partial void OnRegionIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> CapitalID
        {
            get
            {
                return _CapitalID;
            }
            set
            {
                OnCapitalIDChanging(value);
                ReportPropertyChanging("CapitalID");
                _CapitalID = StructuralObject.SetValidValue(value, "CapitalID");
                ReportPropertyChanged("CapitalID");
                OnCapitalIDChanged();
            }
        }
        private Nullable<global::System.Int32> _CapitalID;
        partial void OnCapitalIDChanging(Nullable<global::System.Int32> value);
        partial void OnCapitalIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CountryName
        {
            get
            {
                return _CountryName;
            }
            set
            {
                OnCountryNameChanging(value);
                ReportPropertyChanging("CountryName");
                _CountryName = StructuralObject.SetValidValue(value, true, "CountryName");
                ReportPropertyChanged("CountryName");
                OnCountryNameChanged();
            }
        }
        private global::System.String _CountryName;
        partial void OnCountryNameChanging(global::System.String value);
        partial void OnCountryNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CountryCode
        {
            get
            {
                return _CountryCode;
            }
            set
            {
                OnCountryCodeChanging(value);
                ReportPropertyChanging("CountryCode");
                _CountryCode = StructuralObject.SetValidValue(value, true, "CountryCode");
                ReportPropertyChanged("CountryCode");
                OnCountryCodeChanged();
            }
        }
        private global::System.String _CountryCode;
        partial void OnCountryCodeChanging(global::System.String value);
        partial void OnCountryCodeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Boolean> IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                OnIsActiveChanging(value);
                ReportPropertyChanging("IsActive");
                _IsActive = StructuralObject.SetValidValue(value, "IsActive");
                ReportPropertyChanged("IsActive");
                OnIsActiveChanged();
            }
        }
        private Nullable<global::System.Boolean> _IsActive;
        partial void OnIsActiveChanging(Nullable<global::System.Boolean> value);
        partial void OnIsActiveChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime DateCreated
        {
            get
            {
                return _DateCreated;
            }
            set
            {
                OnDateCreatedChanging(value);
                ReportPropertyChanging("DateCreated");
                _DateCreated = StructuralObject.SetValidValue(value, "DateCreated");
                ReportPropertyChanged("DateCreated");
                OnDateCreatedChanged();
            }
        }
        private global::System.DateTime _DateCreated;
        partial void OnDateCreatedChanging(global::System.DateTime value);
        partial void OnDateCreatedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                OnCreatedByChanging(value);
                ReportPropertyChanging("CreatedBy");
                _CreatedBy = StructuralObject.SetValidValue(value, "CreatedBy");
                ReportPropertyChanged("CreatedBy");
                OnCreatedByChanged();
            }
        }
        private global::System.Int32 _CreatedBy;
        partial void OnCreatedByChanging(global::System.Int32 value);
        partial void OnCreatedByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime DateUpdated
        {
            get
            {
                return _DateUpdated;
            }
            set
            {
                OnDateUpdatedChanging(value);
                ReportPropertyChanging("DateUpdated");
                _DateUpdated = StructuralObject.SetValidValue(value, "DateUpdated");
                ReportPropertyChanged("DateUpdated");
                OnDateUpdatedChanged();
            }
        }
        private global::System.DateTime _DateUpdated;
        partial void OnDateUpdatedChanging(global::System.DateTime value);
        partial void OnDateUpdatedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 UpdatedBy
        {
            get
            {
                return _UpdatedBy;
            }
            set
            {
                OnUpdatedByChanging(value);
                ReportPropertyChanging("UpdatedBy");
                _UpdatedBy = StructuralObject.SetValidValue(value, "UpdatedBy");
                ReportPropertyChanged("UpdatedBy");
                OnUpdatedByChanged();
            }
        }
        private global::System.Int32 _UpdatedBy;
        partial void OnUpdatedByChanging(global::System.Int32 value);
        partial void OnUpdatedByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> DivisionID
        {
            get
            {
                return _DivisionID;
            }
            set
            {
                OnDivisionIDChanging(value);
                ReportPropertyChanging("DivisionID");
                _DivisionID = StructuralObject.SetValidValue(value, "DivisionID");
                ReportPropertyChanged("DivisionID");
                OnDivisionIDChanged();
            }
        }
        private Nullable<global::System.Int32> _DivisionID;
        partial void OnDivisionIDChanging(Nullable<global::System.Int32> value);
        partial void OnDivisionIDChanged();

        #endregion

        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_States_Countries", "State")]
        public EntityCollection<State> States
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<State>("Model.FK_States_Countries", "State");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<State>("Model.FK_States_Countries", "State", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="State")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class State : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new State object.
        /// </summary>
        /// <param name="stateID">Initial value of the StateID property.</param>
        /// <param name="countryID">Initial value of the CountryID property.</param>
        /// <param name="capitalID">Initial value of the CapitalID property.</param>
        /// <param name="stateName">Initial value of the StateName property.</param>
        /// <param name="dateCreated">Initial value of the DateCreated property.</param>
        /// <param name="createdBy">Initial value of the CreatedBy property.</param>
        /// <param name="dateUpdated">Initial value of the DateUpdated property.</param>
        /// <param name="updatedBy">Initial value of the UpdatedBy property.</param>
        public static State CreateState(global::System.Int32 stateID, global::System.Int16 countryID, global::System.Int32 capitalID, global::System.String stateName, global::System.DateTime dateCreated, global::System.Int32 createdBy, global::System.DateTime dateUpdated, global::System.Int32 updatedBy)
        {
            State state = new State();
            state.StateID = stateID;
            state.CountryID = countryID;
            state.CapitalID = capitalID;
            state.StateName = stateName;
            state.DateCreated = dateCreated;
            state.CreatedBy = createdBy;
            state.DateUpdated = dateUpdated;
            state.UpdatedBy = updatedBy;
            return state;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 StateID
        {
            get
            {
                return _StateID;
            }
            set
            {
                OnStateIDChanging(value);
                ReportPropertyChanging("StateID");
                _StateID = StructuralObject.SetValidValue(value, "StateID");
                ReportPropertyChanged("StateID");
                OnStateIDChanged();
            }
        }
        private global::System.Int32 _StateID;
        partial void OnStateIDChanging(global::System.Int32 value);
        partial void OnStateIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int16 CountryID
        {
            get
            {
                return _CountryID;
            }
            set
            {
                if (_CountryID != value)
                {
                    OnCountryIDChanging(value);
                    ReportPropertyChanging("CountryID");
                    _CountryID = StructuralObject.SetValidValue(value, "CountryID");
                    ReportPropertyChanged("CountryID");
                    OnCountryIDChanged();
                }
            }
        }
        private global::System.Int16 _CountryID;
        partial void OnCountryIDChanging(global::System.Int16 value);
        partial void OnCountryIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 CapitalID
        {
            get
            {
                return _CapitalID;
            }
            set
            {
                if (_CapitalID != value)
                {
                    OnCapitalIDChanging(value);
                    ReportPropertyChanging("CapitalID");
                    _CapitalID = StructuralObject.SetValidValue(value, "CapitalID");
                    ReportPropertyChanged("CapitalID");
                    OnCapitalIDChanged();
                }
            }
        }
        private global::System.Int32 _CapitalID;
        partial void OnCapitalIDChanging(global::System.Int32 value);
        partial void OnCapitalIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String StateName
        {
            get
            {
                return _StateName;
            }
            set
            {
                if (_StateName != value)
                {
                    OnStateNameChanging(value);
                    ReportPropertyChanging("StateName");
                    _StateName = StructuralObject.SetValidValue(value, false, "StateName");
                    ReportPropertyChanged("StateName");
                    OnStateNameChanged();
                }
            }
        }
        private global::System.String _StateName;
        partial void OnStateNameChanging(global::System.String value);
        partial void OnStateNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String AreaCode
        {
            get
            {
                return _AreaCode;
            }
            set
            {
                OnAreaCodeChanging(value);
                ReportPropertyChanging("AreaCode");
                _AreaCode = StructuralObject.SetValidValue(value, true, "AreaCode");
                ReportPropertyChanged("AreaCode");
                OnAreaCodeChanged();
            }
        }
        private global::System.String _AreaCode;
        partial void OnAreaCodeChanging(global::System.String value);
        partial void OnAreaCodeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Boolean> IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                OnIsActiveChanging(value);
                ReportPropertyChanging("IsActive");
                _IsActive = StructuralObject.SetValidValue(value, "IsActive");
                ReportPropertyChanged("IsActive");
                OnIsActiveChanged();
            }
        }
        private Nullable<global::System.Boolean> _IsActive;
        partial void OnIsActiveChanging(Nullable<global::System.Boolean> value);
        partial void OnIsActiveChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime DateCreated
        {
            get
            {
                return _DateCreated;
            }
            set
            {
                OnDateCreatedChanging(value);
                ReportPropertyChanging("DateCreated");
                _DateCreated = StructuralObject.SetValidValue(value, "DateCreated");
                ReportPropertyChanged("DateCreated");
                OnDateCreatedChanged();
            }
        }
        private global::System.DateTime _DateCreated;
        partial void OnDateCreatedChanging(global::System.DateTime value);
        partial void OnDateCreatedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                OnCreatedByChanging(value);
                ReportPropertyChanging("CreatedBy");
                _CreatedBy = StructuralObject.SetValidValue(value, "CreatedBy");
                ReportPropertyChanged("CreatedBy");
                OnCreatedByChanged();
            }
        }
        private global::System.Int32 _CreatedBy;
        partial void OnCreatedByChanging(global::System.Int32 value);
        partial void OnCreatedByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime DateUpdated
        {
            get
            {
                return _DateUpdated;
            }
            set
            {
                OnDateUpdatedChanging(value);
                ReportPropertyChanging("DateUpdated");
                _DateUpdated = StructuralObject.SetValidValue(value, "DateUpdated");
                ReportPropertyChanged("DateUpdated");
                OnDateUpdatedChanged();
            }
        }
        private global::System.DateTime _DateUpdated;
        partial void OnDateUpdatedChanging(global::System.DateTime value);
        partial void OnDateUpdatedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 UpdatedBy
        {
            get
            {
                return _UpdatedBy;
            }
            set
            {
                OnUpdatedByChanging(value);
                ReportPropertyChanging("UpdatedBy");
                _UpdatedBy = StructuralObject.SetValidValue(value, "UpdatedBy");
                ReportPropertyChanged("UpdatedBy");
                OnUpdatedByChanged();
            }
        }
        private global::System.Int32 _UpdatedBy;
        partial void OnUpdatedByChanging(global::System.Int32 value);
        partial void OnUpdatedByChanged();

        #endregion

        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_States_Countries", "Country")]
        public ExternalCountry Country
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ExternalCountry>("Model.FK_States_Countries", "Country").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ExternalCountry>("Model.FK_States_Countries", "Country").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<ExternalCountry> CountryReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ExternalCountry>("Model.FK_States_Countries", "Country");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<ExternalCountry>("Model.FK_States_Countries", "Country", value);
                }
            }
        }

        #endregion

    }

    #endregion

}
