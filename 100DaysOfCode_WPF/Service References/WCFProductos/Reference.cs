﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _100DaysOfCode_WPF.WCFProductos {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Producto", Namespace="http://schemas.datacontract.org/2004/07/_100DaysOfCode_WCF")]
    [System.SerializableAttribute()]
    public partial class Producto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int cantidadField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string codigoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] imagenField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nombreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double precioField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string rutaImagenField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int cantidad {
            get {
                return this.cantidadField;
            }
            set {
                if ((this.cantidadField.Equals(value) != true)) {
                    this.cantidadField = value;
                    this.RaisePropertyChanged("cantidad");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string codigo {
            get {
                return this.codigoField;
            }
            set {
                if ((object.ReferenceEquals(this.codigoField, value) != true)) {
                    this.codigoField = value;
                    this.RaisePropertyChanged("codigo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int id {
            get {
                return this.idField;
            }
            set {
                if ((this.idField.Equals(value) != true)) {
                    this.idField = value;
                    this.RaisePropertyChanged("id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] imagen {
            get {
                return this.imagenField;
            }
            set {
                if ((object.ReferenceEquals(this.imagenField, value) != true)) {
                    this.imagenField = value;
                    this.RaisePropertyChanged("imagen");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nombre {
            get {
                return this.nombreField;
            }
            set {
                if ((object.ReferenceEquals(this.nombreField, value) != true)) {
                    this.nombreField = value;
                    this.RaisePropertyChanged("nombre");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double precio {
            get {
                return this.precioField;
            }
            set {
                if ((this.precioField.Equals(value) != true)) {
                    this.precioField = value;
                    this.RaisePropertyChanged("precio");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string rutaImagen {
            get {
                return this.rutaImagenField;
            }
            set {
                if ((object.ReferenceEquals(this.rutaImagenField, value) != true)) {
                    this.rutaImagenField = value;
                    this.RaisePropertyChanged("rutaImagen");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCFProductos.IWCFProducto")]
    public interface IWCFProducto {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFProducto/BuscarProductos", ReplyAction="http://tempuri.org/IWCFProducto/BuscarProductosResponse")]
        _100DaysOfCode_WPF.WCFProductos.Producto[] BuscarProductos(_100DaysOfCode_WPF.WCFProductos.Producto oProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFProducto/BuscarProductos", ReplyAction="http://tempuri.org/IWCFProducto/BuscarProductosResponse")]
        System.Threading.Tasks.Task<_100DaysOfCode_WPF.WCFProductos.Producto[]> BuscarProductosAsync(_100DaysOfCode_WPF.WCFProductos.Producto oProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFProducto/AgregarProducto", ReplyAction="http://tempuri.org/IWCFProducto/AgregarProductoResponse")]
        string AgregarProducto(_100DaysOfCode_WPF.WCFProductos.Producto oProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFProducto/AgregarProducto", ReplyAction="http://tempuri.org/IWCFProducto/AgregarProductoResponse")]
        System.Threading.Tasks.Task<string> AgregarProductoAsync(_100DaysOfCode_WPF.WCFProductos.Producto oProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFProducto/ModificarProducto", ReplyAction="http://tempuri.org/IWCFProducto/ModificarProductoResponse")]
        string ModificarProducto(_100DaysOfCode_WPF.WCFProductos.Producto oProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFProducto/ModificarProducto", ReplyAction="http://tempuri.org/IWCFProducto/ModificarProductoResponse")]
        System.Threading.Tasks.Task<string> ModificarProductoAsync(_100DaysOfCode_WPF.WCFProductos.Producto oProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFProducto/EliminarProducto", ReplyAction="http://tempuri.org/IWCFProducto/EliminarProductoResponse")]
        string EliminarProducto(_100DaysOfCode_WPF.WCFProductos.Producto oProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFProducto/EliminarProducto", ReplyAction="http://tempuri.org/IWCFProducto/EliminarProductoResponse")]
        System.Threading.Tasks.Task<string> EliminarProductoAsync(_100DaysOfCode_WPF.WCFProductos.Producto oProducto);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWCFProductoChannel : _100DaysOfCode_WPF.WCFProductos.IWCFProducto, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WCFProductoClient : System.ServiceModel.ClientBase<_100DaysOfCode_WPF.WCFProductos.IWCFProducto>, _100DaysOfCode_WPF.WCFProductos.IWCFProducto {
        
        public WCFProductoClient() {
        }
        
        public WCFProductoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WCFProductoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WCFProductoClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WCFProductoClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public _100DaysOfCode_WPF.WCFProductos.Producto[] BuscarProductos(_100DaysOfCode_WPF.WCFProductos.Producto oProducto) {
            return base.Channel.BuscarProductos(oProducto);
        }
        
        public System.Threading.Tasks.Task<_100DaysOfCode_WPF.WCFProductos.Producto[]> BuscarProductosAsync(_100DaysOfCode_WPF.WCFProductos.Producto oProducto) {
            return base.Channel.BuscarProductosAsync(oProducto);
        }
        
        public string AgregarProducto(_100DaysOfCode_WPF.WCFProductos.Producto oProducto) {
            return base.Channel.AgregarProducto(oProducto);
        }
        
        public System.Threading.Tasks.Task<string> AgregarProductoAsync(_100DaysOfCode_WPF.WCFProductos.Producto oProducto) {
            return base.Channel.AgregarProductoAsync(oProducto);
        }
        
        public string ModificarProducto(_100DaysOfCode_WPF.WCFProductos.Producto oProducto) {
            return base.Channel.ModificarProducto(oProducto);
        }
        
        public System.Threading.Tasks.Task<string> ModificarProductoAsync(_100DaysOfCode_WPF.WCFProductos.Producto oProducto) {
            return base.Channel.ModificarProductoAsync(oProducto);
        }
        
        public string EliminarProducto(_100DaysOfCode_WPF.WCFProductos.Producto oProducto) {
            return base.Channel.EliminarProducto(oProducto);
        }
        
        public System.Threading.Tasks.Task<string> EliminarProductoAsync(_100DaysOfCode_WPF.WCFProductos.Producto oProducto) {
            return base.Channel.EliminarProductoAsync(oProducto);
        }
    }
}