// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace KinskyTouch {
	
	
	// Base type probably should be UIKit.UIViewController or subclass
	[Foundation.Register("CellBrowserHeaderFactory")]
	public partial class CellBrowserHeaderFactory {
		
		private CellBrowserHeader __mt_cellBrowserHeader;
		
		#pragma warning disable 0169
		[Foundation.Connect("cellBrowserHeader")]
		private CellBrowserHeader cellBrowserHeader {
			get {
				this.__mt_cellBrowserHeader = ((CellBrowserHeader)(this.GetNativeField("cellBrowserHeader")));
				return this.__mt_cellBrowserHeader;
			}
			set {
				this.__mt_cellBrowserHeader = value;
				this.SetNativeField("cellBrowserHeader", value);
			}
		}
	}
	
	// Base type probably should be UIKit.UITableViewCell or subclass
	[Foundation.Register("CellBrowserHeader")]
	public partial class CellBrowserHeader {
		
		private UIKit.UIImageView __mt_imageViewArtwork;
		
		private UIKit.UILabel __mt_labelTitle;
		
		private UIKit.UILabel __mt_labelArtistAlbum;
		
		private UIKit.UILabel __mt_labelComposer;
		
		#pragma warning disable 0169
		[Foundation.Connect("imageViewArtwork")]
		private UIKit.UIImageView imageViewArtwork {
			get {
				this.__mt_imageViewArtwork = ((UIKit.UIImageView)(this.GetNativeField("imageViewArtwork")));
				return this.__mt_imageViewArtwork;
			}
			set {
				this.__mt_imageViewArtwork = value;
				this.SetNativeField("imageViewArtwork", value);
			}
		}
		
		[Foundation.Connect("labelTitle")]
		private UIKit.UILabel labelTitle {
			get {
				this.__mt_labelTitle = ((UIKit.UILabel)(this.GetNativeField("labelTitle")));
				return this.__mt_labelTitle;
			}
			set {
				this.__mt_labelTitle = value;
				this.SetNativeField("labelTitle", value);
			}
		}
		
		[Foundation.Connect("labelArtistAlbum")]
		private UIKit.UILabel labelArtistAlbum {
			get {
				this.__mt_labelArtistAlbum = ((UIKit.UILabel)(this.GetNativeField("labelArtistAlbum")));
				return this.__mt_labelArtistAlbum;
			}
			set {
				this.__mt_labelArtistAlbum = value;
				this.SetNativeField("labelArtistAlbum", value);
			}
		}
		
		[Foundation.Connect("labelComposer")]
		private UIKit.UILabel labelComposer {
			get {
				this.__mt_labelComposer = ((UIKit.UILabel)(this.GetNativeField("labelComposer")));
				return this.__mt_labelComposer;
			}
			set {
				this.__mt_labelComposer = value;
				this.SetNativeField("labelComposer", value);
			}
		}
	}
}