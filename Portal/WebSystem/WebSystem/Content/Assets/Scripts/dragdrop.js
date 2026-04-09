// For Collapse/Expand Feature
togglePanel = function(objToggle, obj) {
$(objToggle).siblings('.ItemContainerDragBox-Content').toggle();

    if ($(objToggle).siblings('.ItemContainerDragBox-Content').is(':hidden'))
    	obj.src = "/Content/Assets/Images/common/expandpanel.gif";
    else
        obj.src = "/Content/Assets/Images/common/collapsepanel.gif";
}


$(function() {
	// For Collapse/Expand Feature
//    $('.ItemContainerDragBox')
//				.each(function() {
//				    $(this).hover(function() {
//					.click(function() {
//					$(this).siblings('.ItemContainerDragBox-Content').toggle();
//					})
//					.end()
//				});
$('.ItemContainer').sortable({
	connectWith: '.ItemContainer',
		handle: 'table',
		cursor: 'move',
		placeholder: 'ItemContainerPlaceholder',
		forcePlaceholderSize: true,
		opacity: 0.4,
		stop: function(event, ui) {
			$(ui.item).find('table').click();
			/*
			var sortorder = '';
			$('.ItemContainer').each(function() {
				var itemorder = $(this).sortable('toArray');
				var columnId = $(this).attr('id');
				sortorder += columnId + '=' + itemorder.toString() + '&';
			});
			document.getElementById('__hidPanelOrder').value = sortorder;
			alert('SortOrder: ' + sortorder);
			*/
			/*Pass sortorder variable to server using ajax to save state*/
		}
	})
    .disableSelection();
});