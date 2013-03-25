var slider = {
	is_initiated 			: false,
	content_container		: false,
	content_slider 			: false,
	content_slides			: 0,
	pos						: 0,
	amount					: 300,
	offset					: 3,
	speed					: 280,
	
	move : function ( direction, sender ) {
		// initiate if not already initiated...
		if ( ! slider.is_initiated ) {
			slider.initiate();
		}
		
		// move
		if ( direction == -1 ) {
			if ( ( slider.pos + slider.offset ) < slider.content_slides ) {
				slider.pos++;
			} else {
				return; // should not get to this point as buttons are hidden 
			}
		} else {
			if ( ( slider.pos - 1 ) >= 0 ) {
				slider.pos--;
			} else {
				return; // should not get to this point as buttons are hidden 
			}
		}
		
		// hide left arrow when reached left end
		if ( slider.pos > 0 ) { $('.nav-arrow-left').fadeIn(); } else { $('.nav-arrow-left').fadeOut(); }
		
		// hide right arrow when reached right end
		if ( ( slider.pos + slider.offset ) < slider.content_slides ) { $('.nav-arrow-right').fadeIn(); } else { $('.nav-arrow-right').fadeOut(); }
		
		// animate
		slider.content_slider.stop().animate( { left: ( slider.amount * slider.pos ) * -1 }, slider.speed );
	},
	
	// this function should be run to reset the slider when page is resized to recalc all vars...
	initiate : function () {
		
		// only need to run once to find all specific elements
		if ( ! slider.initiated ) {
			slider.content_container	= slider.content_container === false ? $('.content-container') : slider.content_container;
			slider.content_slider 		= slider.content_slider === false ? $('.content-slider') : slider.content_slider;
			slider.content_slides 		= slider.content_slider.children().length;
		}
		
		// set pos to zero
		slider.pos = 0;
		
		// reset slider to first position
		slider.content_slider.css('left', 0)
		
		// calculate the amount the slider should move
		slider.amount = slider.content_slider.children('div.content-instance').first().innerWidth();
		
		// set width of container to number of elements
		slider.content_slider.css( 'width', ( slider.content_slides * slider.amount ) );
		
		// set offset based on the layout in use
		switch ( attain.layout ) {
			case 'mobile':
			case 'wide_mobile':
				slider.offset = 1;
			break;
			case 'tablet':
				slider.offset = 2;
			break;
			default:
				slider.offset = 3;
		}
		
		// hide left arrow initially
		$('.nav-arrow-left').hide();
		
		// hide right arrow if less than or equal to the offset, else fade in...
		if ( slider.content_slides <= slider.offset ) {
			$('.nav-arrow-right').hide();
		} else {
			$('.nav-arrow-right').fadeIn();
		}
		
		// only need to specify event holder once...
		if ( ! slider.is_initiated ) {
			slider.content_container.touchwipe({
				swipeLeft  : function () {
					slider.move(-1, this);
				},
				swipeRight : function () {
					slider.move( 1, this);
				},
				preventDefaultEvents : false
			});
		}
		
		// set initiated to true...
		slider.is_initiated = true;
	}
};

attain.add_onload_function( slider.initiate );

var fader = {
	
	loaded 			: false,
	
	wrapper 		: false,
	container 		: false,
	navigation 		: false,
	slides 			: false,
	images 			: false,
	
	pos 			: 0,
	num_slides 		: 0,
	images_loaded	: 0,
	
	current			: false,
	next			: false,
	
	delay			: 4000,
	speed			: 0,
	
	timer			: false,
	
	initiate : function () {
		if ( ! fader.loaded ) { fader.load(); }
	},
	
	load : function () {
		fader.wrapper		= $('#slider');
		fader.container 	= $('#slides');
		fader.navigation 	= $('#slide_nav');
		fader.slides		= fader.container.children('.slide');
		fader.current 		= fader.slides.first();
		fader.images 		= fader.slides.children('img');
		
		fader.num_slides 	= fader.slides.length;
		
		fader.wrapper.touchwipe({
			swipeLeft  : fader.go_next,
			swipeRight : fader.go_back,
			preventDefaultEvents : false
		});
		
		$(window).load( function () { fader.complete(); } );
		
		fader.container.mouseenter( fader.over ).mouseleave( fader.leave );
	},
	
	complete : function () {
		if ( ! fader.loaded ) {
			fader.current.show();
			
			for ( var i = 0; i < fader.num_slides; i++ ) {
				var a = document.createElement('a');
				
				a.setAttribute('href', 'javascript:fader._goto(' + i + ');');
				
				if ( i == 0 ) { a.setAttribute('class', 'active'); }
				
				fader.navigation.append( a );
			}
			
			fader.timer = setTimeout( fader.play, fader.delay );
		}
		fader.loaded = true;
	},
	
	before_action : function () {
		if ( fader.timer !== false ) { clearTimeout( fader.timer ); }
		
		fader.slides.css('z-index', '20');
		
		fader.navigation.children('a').removeClass();
		
		fader.current = $(fader.slides[ fader.pos ]);
	},
	
	after_action : function ( proceed ) {
		
		proceed = typeof proceed === 'undefined' ? true : proceed;
		
		fader.navigation.children('a').eq( fader.pos ).addClass('active');
		
		fader.next = $(fader.slides[ fader.pos ]);
		
		fader.next.css('z-index', '21').fadeIn(fader.speed, function () {
			fader.current.hide();
			
			if ( proceed ) {
				fader.timer = setTimeout( fader.play, fader.delay );
			}
		});
	},
	
	go_back : function (e) {
		if ( fader.pos > 0 ) {
			fader.before_action();
			
			fader.pos--;
			
			fader.after_action();
		}
		e.stopPropagation();
	},
	
	go_next : function (e) {
		if ( ( fader.pos + 1 ) < fader.num_slides ) {
			fader.before_action();
			
			fader.pos++;
			
			fader.after_action();
		}
		e.stopPropagation();
	},
	
	_goto : function ( new_pos ) {
		if ( new_pos != fader.pos ) {
			fader.before_action();
			
			fader.pos = new_pos;
			
			fader.after_action( false );
		}
	},
	
	play : function () {
		fader.before_action();
		
		if ( ( fader.pos + 1 ) < fader.num_slides  ) {
			fader.pos++;
		} else {
			fader.pos = 0; 
		}
		
		fader.after_action();
	},
	
	over : function () {
		if ( fader.timer !== false ) { clearTimeout( fader.timer ); }
	},
	
	leave : function () {
		fader.timer = setTimeout( fader.play, fader.delay );
	}
};

attain.add_onload_function( fader.initiate );