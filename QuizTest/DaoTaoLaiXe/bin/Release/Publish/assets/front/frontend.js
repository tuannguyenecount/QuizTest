/**
 * Main JS file for Casper behaviours
 */

/*globals jQuery, document */
(function ($) {
    "use strict";

    $(document).ready(function(){
		$('#picker').colpick({
			flat:true,
			layout:'hex',
			submit:0,
			color: '#F15F74',
			onChange: function(hsb,hex,rgb,el,bySetColor){
				$('#header nav,div.home div.timeline article header time,div.home div.timeline div.spine,div.home div.timeline article:hover span.arrow,.pagination div.nav,#footer,div.single div.spine,div.single article header div.meta time,div.single article footer section.share a,nav.mobile').css('background','#'+hex);
				$('div.home div.timeline article span.arrow,div.single article header div.separator,.post-content blockquote,div.single article footer section.tags').css('border-color','#'+hex);
				$('a,#header hgroup h1.logo a,div.home div.timeline article header h2 a,#back-to-top,h1, h2, h3, h4, h5, h6,div.single article header h2,div.single article footer section.share h4,div.single section.author h4 span,div.map h3.location i.fa,div.single article footer section.tags time,div.single article footer section.tags span').css('color','#'+hex);
			}
		});
		$('div#settings a.icon').click(function(){
			$('div#settings').toggleClass('active');
		});
	
    }); // end: document.ready


}(jQuery));