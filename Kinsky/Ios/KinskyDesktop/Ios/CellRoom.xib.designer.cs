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
	[Foundation.Register("CellRoomFactory")]
	public partial class CellRoomFactory {
		
		private CellRoom __mt_cellRoom;
		
		#pragma warning disable 0169
		[Foundation.Connect("cellRoom")]
		private CellRoom cellRoom {
			get {
				this.__mt_cellRoom = ((CellRoom)(this.GetNativeField("cellRoom")));
				return this.__mt_cellRoom;
			}
			set {
				this.__mt_cellRoom = value;
				this.SetNativeField("cellRoom", value);
			}
		}
	}
	
	// Base type probably should be UIKit.UITableViewCell or subclass
	[Foundation.Register("CellRoom")]
	public partial class CellRoom {
		
		private UIKit.UILabel __mt_labelTitle;
		private UIKit.UIButton __mt_buttonStandby;
		
		
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
		
		
		[Foundation.Connect("buttonStandby")]
		private UIKit.UIButton buttonStandby {
			get {
				this.__mt_buttonStandby = ((UIKit.UIButton)(this.GetNativeField("buttonStandby")));
				return this.__mt_buttonStandby;
			}
			set {
				this.__mt_buttonStandby = value;
				this.SetNativeField("buttonStandby", value);
			}
		}
	}
}
