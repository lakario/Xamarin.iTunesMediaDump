
using System;
using System.Collections.Generic;

using Foundation;
using UIKit;
using MediaPlayer;

namespace iTunesMediaDump
{
	public partial class ListMediaController : UITableViewController
	{
		public ListMediaController () : base (UITableViewStyle.Grouped)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.

			var query = new MPMediaQuery ();

			//var predicate = MPMediaPropertyPredicate.PredicateWithValue (FromObject(MPMediaType.Any), MPMediaItem.MediaTypeProperty);
			var predicate = MPMediaPropertyPredicate.PredicateWithValue(FromObject(MPMediaType.Music), MPMediaItem.MediaTypeProperty);
			query.AddFilterPredicate (predicate);


			var items = query.Items;

			TableView.RegisterClassForCellReuse (typeof(UITableViewCell), MediaItemsDataSource.MediaItemCellId);
			TableView.Source = new MediaItemsDataSource (query.Items);
			//TableView.ReloadData()
		}

		class MediaItemsDataSource : UITableViewSource {
			public const string MediaItemCellId = "MediaItemCell";
			private readonly MPMediaItem[] _mediaItems;

			public MediaItemsDataSource (MPMediaItem[] mediaItems)
			{
				_mediaItems = mediaItems;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return _mediaItems != null ? _mediaItems.Length : 0;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(MediaItemCellId);
				if (cell == null || cell.DetailTextLabel == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Subtitle, MediaItemCellId);
				}

				var row = indexPath.Row;

				if (row >= 0 && row < _mediaItems.Length) {
					var mediaItem = _mediaItems[row];

					cell.TextLabel.Text = mediaItem.Artist;
					cell.DetailTextLabel.Text = mediaItem.Title;
				} else {
					cell.TextLabel.Text = null;
					cell.DetailTextLabel.Text = null;
				}

				return cell;
			}
		}
	}
}

