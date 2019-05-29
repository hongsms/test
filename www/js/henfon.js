
$(function(){
	var $heights = $(window).height();
	    $widths  = $(window).width();
		function init(){
			$(".disloc").each(function() {
			var self = $(this);
				setTimeout(function(){
					var offsetTop = self[0].offsetTop;
					  if (offsetTop >= $(window).scrollTop() && offsetTop < ($(window).scrollTop()+$(window).height())) {
							var peos = self.offset().top;
							var lisx = peos-$(window).height();
							if($(window).scrollTop()>lisx){
							  self.addClass("animate");
							}
						}
				},300);
			});
		}
		$(window).scroll(function(){
		 init()
		}).scroll();
		
	
	if($(window).width()>1200){
		$(".T-nav").css({height:"59px"});
		//var ind = $(".T-nav li a.cur").parent().index();
		//alert(ind)
		$(".T-nav li").hover(function(){
			$(this).children("a").addClass("cur");
			$(this).siblings().children("a").removeClass("cur");
			},function(){
				$(this).children("a").removeClass("cur");
				//$(".T-nav li").eq(ind).children("a").addClass("cur");
				})
		
		
		}else{
			$(".T-nav").height($heights-50);
			};

	$(window).resize(function(){
		var mosh = $(window).height();
		if($(window).width()>1200){
			$(".T-nav").css({height:"59px"})
			}else{
				$(".T-nav").height(mosh-50)
				}
		if($(window).width()>1200){
			$(".T-nav").show();
			}else{
				$(".T-nav").hide();
				}
		});
	$(".mob-nav").click(function(){
		$(".T-nav").stop(false,true).fadeToggle();
		});
	
	$(".news-cont li:nth-child(3n)").css({marginRight:"0"});
	$(".tab-hd span").click(function(){
		var slis = $(this).index();
		$(this).addClass("cur").siblings().removeClass("cur");
		$(this).parent().siblings(".tab-bd").children(".bd").eq(slis).stop(false,true).removeClass("helse").siblings().addClass("helse");
		});	
	
	$(".mob-sild").click(function(){
		if($(this).hasClass("backl")){
			$(this).removeClass("backl").stop(false,true).animate({left:"0px"});
			$(".lolt-red").stop(false,true).animate({left:"-250px"})
			}else{
				$(this).addClass("backl").stop(false,true).animate({left:"250px"});
		        $(".lolt-red").stop(false,true).animate({left:"0"});
				}
		
		});
	$(".foot .bon span,.colse span,.midles span").click(function(){
		$(this).siblings().stop(false,true).slideToggle();
		});
	$(".colse span").text($(".colse p a.cur").text());
	$(".preliv li:even").find(".yewat").addClass("fl").addClass("tr");
	$(".preliv li:even").find(".contl").addClass("fr");
	$(".preliv li:odd").find(".yewat").addClass("fr");
	$(".preliv li:odd").find(".contl").addClass("fl");
	$(".younle tbody tr:odd").addClass("odd");
	if($(window).width()>1280){
			$(".melixt").css({height:"513px"})
			
			}else{
				$(".melixt").css({height:$heights-180});
				};
		$(window).resize(function(){
			if($(window).width()>1280){
				$(".melixt").css({height:"513px"});
				}else{
					$(".melixt").css({height:$heights-180});
					}
			})	
	$(".mid-pics > a").hover(function(){
		var kew = $(this).index();
		$(".mid-pics span a").eq(kew).toggleClass("cur");
		})
    $(".flr04").click(function(){
		$('html,body').animate({scrollTop:0});
		})
})


















