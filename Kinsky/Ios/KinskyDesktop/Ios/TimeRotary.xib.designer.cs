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
	[Foundation.Register("ViewWidgetTimeRotary")]
	public partial class ViewWidgetTimeRotary {
		
		private UIKit.UIView __mt_view;
		
		private UIControlRotary __mt_controlRotary;
		
		private UIKit.UIImageView __mt_imageViewElapsed;
		
		private UIKit.UIImageView __mt_imageViewRemaining;
		
		#pragma warning disable 0169
		[Foundation.Connect("view")]
		private UIKit.UIView view {
			get {
				this.__mt_view = ((UIKit.UIView)(this.GetNativeField("view")));
				return this.__mt_view;
			}
			set {
				this.__mt_view = value;
				this.SetNativeField("view", value);
			}
		}
		
		[Foundation.Connect("controlRotary")]
		private UIControlRotary controlRotary {
			get {
				this.__mt_controlRotary = ((UIControlRotary)(this.GetNativeField("controlRotary")));
				return this.__mt_controlRotary;
			}
			set {
				this.__mt_controlRotary = value;
				this.SetNativeField("controlRotary", value);
			}
		}
		
		[Foundation.Connect("imageViewElapsed")]
		private UIKit.UIImageView imageViewElapsed {
			get {
				this.__mt_imageViewElapsed = ((UIKit.UIImageView)(this.GetNativeField("imageViewElapsed")));
				return this.__mt_imageViewElapsed;
			}
			set {
				this.__mt_imageViewElapsed = value;
				this.SetNativeField("imageViewElapsed", value);
			}
		}
		
		[Foundation.Connect("imageViewRemaining")]
		private UIKit.UIImageView imageViewRemaining {
			get {
				this.__mt_imageViewRemaining = ((UIKit.UIImageView)(this.GetNativeField("imageViewRemaining")));
				return this.__mt_imageViewRemaining;
			}
			set {
				this.__mt_imageViewRemaining = value;
				this.SetNativeField("imageViewRemaining", value);
			}
		}
	}
}
