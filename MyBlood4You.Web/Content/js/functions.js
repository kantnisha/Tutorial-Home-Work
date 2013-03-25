var attain =  {
	layout 				: 'default',
	firstrun			: true,
	_onload_call_backs 	: [],
	
	add_onload_function : function ( func ) {
		if ( typeof func == 'function' ) {
			attain._onload_call_backs.push( func );
		}
	},
	
	process_onload_functions : function () {
		for ( var i = 0; i < attain._onload_call_backs.length; i++ ) {
			var _function = attain._onload_call_backs[i];
			if ( typeof _function == 'function' ) { _function(); }
		}
	},
	
	log : function (str) {
		if ( typeof console !== 'undefined' && typeof console.log !== 'undefined' ) {
			console.log(str);
		}
	},
	
	initiate : function () {
		
		var current_layout = attain.layout;
		var wrapper_width  = $('.wrapper').width();
		
		if ( wrapper_width < 261 ) {
			attain.layout = 'mobile';
		} else if ( wrapper_width < 580 ) {
			attain.layout = 'wide_mobile';
		} else if ( wrapper_width >= 580 && wrapper_width < 600 ) {
			attain.layout = 'tablet';
		} else {
			attain.layout = 'default';
		}
		
		if ( current_layout != attain.layout || attain.firstrun ) {
			
			attain.log( attain.firstrun ? 'Initiating...' : 'Refreshing...');
			
			attain.log('Current Layout: \''+attain.layout+'\'');
			
			if ( attain.firstrun ) {
				
				attain.log('Executing initial run functions...');
				
				// setup resize event to callback on this function
				$(window).resize( attain.initiate );
				attain.log('Windows resize event initialised');
				
				// place holders...
				$('.placeholder').placeholder();
				attain.log('Placeholders initialised');
				
				// setup default callbacks on refresh...
				attain.add_onload_function( menu.initiate );
				attain.add_onload_function( twitter.initiate );
				//attain.add_onload_function( footerdock.initiate );
			}
			
			attain.firstrun = false;
			attain.process_onload_functions();
			attain.log('On load/on resize events executed successfully');
		}
	}
};

$(document).ready( attain.initiate );

var menu = {
	nav					: false,
	drop_down			: false,
	drop_down_children	: false,
	
	menu_visible		: false,
	loaded 				: false, 
	
	// actions for tablet and default view...
	_default : function () {
		//show the nav...
		menu.nav.show();
		
		// hide ul childs
		menu.drop_down_children.hide();
		
		// bind the hover event if not already bound
		if ( ! menu.drop_down.hasEvent('mouseenter') ) {
			menu.drop_down.hover(function () {
				$(this).children('ul').stop(true, true).slideDown(180);
				$(this).children('a').addClass('hovered')
			}, function () {
				$(this).children('ul').hide();
				$(this).children('a').removeClass('hovered')
			});
		}
	},
	
	// actions for mobile views...
	_mobile : function () {
		
		// reset the visibility to false and remove any css classes
		menu.menu_visible = false;
		$('#show-nav').text('Show Menu').removeClass();
		
		// hide the nav
		menu.nav.hide();
		
		// show ul childs
		menu.drop_down_children.show();
		
		// unbind the hover event
		menu.drop_down.unbind('mouseenter mouseleave');
	},
	
	// toggle view of the menu
	toggle_menu : function (sender) {
		if ( attain.layout != 'tablet' && attain.layout != 'default' ) {	
			if ( menu.menu_visible ) {
				$('nav').slideUp('fast');
				$(sender).text('Show Menu').removeClass();
			} else {
				$('nav').slideDown('fast');
				$(sender).text('Hide Menu').addClass('show-nav-active');
			}
			
			menu.menu_visible = ! menu.menu_visible;
		}
	},
	
	initiate : function () {
	
		if ( ! menu.loaded ) {
			menu.nav 					= menu.nav 					!== false ? menu.nav				: $('nav') ;
			menu.drop_down				= menu.drop_down			!== false ? menu.drop_down			: menu.nav.find('li.drop-down')
			menu.drop_down_children		= menu.drop_down_children	!== false ? menu.drop_down_children	: menu.drop_down.children('ul');
			
			menu.drop_down.children('a').prop('href', 'javascript:void(0);').toggle(function () {
				$(this).parent().children('ul').stop(true, true).slideDown(180);
				$(this).parent().children('a').addClass('hovered')
			}, function () {
				$(this).parent().children('ul').hide();
				$(this).parent().children('a').removeClass('hovered')
			});
			
			menu.loaded = true;
			
			attain.log('Menu initialised');
		}
		
		if ( attain.layout == 'tablet' || attain.layout == 'default' ) {
			menu._default();
		} else {
			menu._mobile();
		}
	}
};

/* Footer dock */
var footerdock = {
	
	_full 	: 80,
	_medi 	: 70,
	_mini 	: 60,
	_length : 180,
	
	animate_next_prev 	: true,
	
	_tooltip			: false,
	_pos				: false,
	_tooltip_fadein		: 180,
	_tooltip_delay		: 240,
	_tooltip_timer		: false,
	
	loaded				: false,
	
	hover : function (el) {
		$(el).stop().animate({
			width	: footerdock._full,
			height	: footerdock._full
		}, footerdock._length, function () {
			
			if ( footerdock._tooltip_timer !== false ) {
				clearTimeout( footerdock._tooltip_timer );
			}
			
			footerdock._pos = $(el).position();
			
			footerdock._pos.left -= 20;
			footerdock._pos.top  -= 24;
			
			if ( ! $(el).data('title') ) {
				$(el).data('title', $(el).children('img').attr('title') );
				$(el).children('img').attr('title', '');
			}
			
			footerdock._tooltip.css(footerdock._pos).text( $(this).data('title') );
			
			footerdock._tooltip_timer = setTimeout(function () {
				footerdock._tooltip.fadeIn( footerdock._tooltip_fadein );
			}, footerdock._tooltip_delay );
		});
		
		
		if ( footerdock.animate_next_prev ) {
			$(el).next('a').stop().animate({
				width	: footerdock._medi,
				height	: footerdock._medi
			}, footerdock._length);
			
			$(el).prev('a').stop().animate({
				width	:footerdock._medi,
				height	:footerdock._medi
			}, footerdock._length);
		}
	},
	
	unhover : function (el) {
		if ( footerdock._tooltip_timer !== false ) {
			clearTimeout( footerdock._tooltip_timer );
		}
		
		footerdock._tooltip.hide();
		
		$(el).stop().animate({
			width 	: footerdock._mini,
			height	: footerdock._mini
		}, footerdock._length);
		
		if ( footerdock.animate_next_prev ) {
			$(el).next('a').stop().animate({
				width	: footerdock._mini,
				height	: footerdock._mini
			}, footerdock._length);
			
			$(el).prev('a').stop().animate({
				width	: footerdock._mini,
				height	: footerdock._mini
			}, footerdock._length);
		}
	},
	
	initiate : function () {
		if ( attain.layout == 'default' || attain.layout == 'tablet' ) {
			if ( ! footerdock.loaded ) {
				footerdock._tooltip = $('#footer-tool-tip');
				
				$('.footer-dock a').hover(
					function () {
						footerdock.hover(this);
					},
					function () {
						footerdock.unhover(this);
					}
				);
				
				footerdock.loaded = true;
				
				attain.log('Footer dock initialised');
			}
		}
	}
};

var twitter = {
	loaded : false,
	
	initiate : function () {
		
		// if not loaded prev'ly, load the latest tweet
		if ( twitter.loaded == false ) {
			
			//if ( ! $.browser.msie ) {
				// fetch tweet...
				$.getJSON("http://api.twitter.com/1/statuses/user_timeline.json?screen_name=attaindesign&count=1&callback=?&include_rts=1", function(data) {
					
					// load data...
					$.each(data, function( i, tweet ) {
						$('.twitter-name').text( tweet.user.name + ': ' );
						$('.twitter-time').text( twitter.calc_time( tweet.created_at  ) );
						$('.twitter-msg').html( twitter.parse_links( tweet.text ) );
					});
					
					// fade out loader, and fade in tweet
					$('.twitter-loader').fadeOut('fast', function () {
						// dependent on layout...
						switch( attain.layout ) {
							case 'mobile':
							case 'wide_mobile':
							case 'tablet':
								$('.sm-twitter table td span.twitter-msg').fadeIn('fast');
							break;
							case 'default':
								$('.sm-twitter table td span.twitter-name').fadeIn('fast');
								$('.sm-twitter table td span.twitter-time').fadeIn('fast');
								$('.sm-twitter table td span.twitter-msg').fadeIn('fast');
							break;
						}
					});
				});
				
				// is now loaded...
				twitter.loaded = true;
				attain.log('Twitter initialised');
			//} else {
			//	$('.social-media').hide();
			//}
			
		} else {
			// will only switch when layout changed...
			switch( attain.layout ) {
				case 'mobile':
				case 'wide_mobile':
				case 'tablet':
					$('.twitter-name').hide();
					$('.twitter-time').hide();
				break;
				case 'default':
					$('.twitter-name').show();
					$('.twitter-time').show();
				break;
			}
		}
	},
	
	// calculate the string for relative time since tweeted...
	calc_time : function ( date ) {
		// time now, and when tweeted...
		var created = new Date( twitter.parse_date(date) ).getTime();
		var now		= new Date().getTime();
		
		// difference...
		var timeago = Math.floor( ( now - created ) / 1000 );
		
		// logic calc...
		if 			( timeago < 60) return 'less than a minute ago';
		else if 	( timeago < 120 ) return 'about a minute ago';
		else if 	( timeago < ( 45 * 60 ) ) return ( parseInt( timeago / 60 ) ).toString() + ' minutes ago';
		else if 	( timeago < ( 90 * 60 ) ) return 'about an hour ago';
		else if 	( timeago < ( 24 * 60 * 60 ) ) return 'about ' + ( parseInt( timeago / 3600 ) ).toString() + ' hours ago';
		else if 	( timeago < ( 48 * 60 * 60 ) ) return '1 day ago';
		else return ( parseInt  ( timeago / 86400 ) ).toString() + ' days ago';
	},
	
	// this function is needed to ensure cross browser data compatibilty (ie, lol)
	parse_date : function ( date ) {
		var v = date.split(' ');
		return Date.parse(v[1]+" "+v[2]+", "+v[5]+" "+v[3]+" GMT");
	},
	
	// parse links in the twitter feed and create a tags...
	parse_links : function ( text ) {
		var exp = /(\b(https?|ftp|file):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/ig;
		return text.replace(exp,"<a href=\"$1\">$1</a>");
	}
};

var fb = {
	init : function () {
		$("#feedback-link").colorbox();
	}
};

attain.add_onload_function( fb.init );