@model TemplateContext<Presentation>

@{
    Presentation contextObject = Model.Object;
    var animateTransitions = Model.Global.Get<bool>("animateTransitions").ToString().ToLower();
    var pagesCount = contextObject.Slides.Count;
    var slideWidth = contextObject.SlideSize.Size.Width;
    var slideHeight = contextObject.SlideSize.Size.Height;
}


var currentVisiblePage = 1;
var maxVisiblePage = @pagesCount;
var frameWidth = @slideWidth;
var frameHeight = @slideHeight;
var polygonsCache = {};

$(document).ready(function(){
      
    if (@animateTransitions) {
        InitTransitions();
    }
});

function InitTransitions() {
    PlayTransition('#slide-1', null);
    
    $('#PrevSlide').show();
    $('#NextSlide').show();
      
    $("#PrevSlide").click(function(){ ShowPrev(); });
    $("#NextSlide").click(function(){ ShowNext(); });
}

function PlayTransition(slideId, prevSlideId) {
    console.log('move from ' + prevSlideId + ' to ' + slideId);
    PlayTransitionBegin();

    var slide = $(slideId);
    var transitionType = slide.data('transitionType');
    var direction = slide.data("transitionDirection");
    
    ResetAnimationProperties();
    
    // assertions on the start of the transition:
    //   prevSlide is visible
    //   slide is hidden
    if(transitionType === 'Fade') {
        Fade(slideId, prevSlideId); 
    } else if(transitionType === 'Push' || transitionType === 'Pan') {
        PushPan(slideId, prevSlideId, transitionType);
    } else if(transitionType === 'Pull') {
        Pull(slideId, prevSlideId);
    } else if(transitionType === 'Cover') {
        Cover(slideId, prevSlideId);
    } else if(transitionType === 'RandomBar') {
        RandomBar(slideId, prevSlideId);
    } else if(transitionType === 'Flash') {
        Flash(slideId, prevSlideId);
    } else if(transitionType === 'Wheel' || transitionType === 'WheelReverse' || transitionType === 'Wedge') {
        Wheel(slideId, prevSlideId, transitionType);
    } else if(transitionType === 'Dissolve') {
        Dissolve(slideId, prevSlideId);
    } else if(transitionType === 'Circle' || transitionType === 'Diamond' || transitionType === 'Plus') {
        Shape(slideId, prevSlideId, transitionType);
    } else if(transitionType === 'Zoom') {
        Shape(slideId, prevSlideId, transitionType + direction);
    } else if(transitionType === 'Split') {
        Split(slideId, prevSlideId);
    } else if(transitionType === 'Wipe') {
        Wipe(slideId, prevSlideId);
    } else if(transitionType === 'Cube' || transitionType === 'Box' || transitionType === 'Rotate' || transitionType === 'Orbit') {
        CubeBoxRotateOrbit(slideId, prevSlideId, transitionType);
    } else if(transitionType === 'Gallery' || transitionType === 'Conveyor') {
        GalleryConveyor(slideId, prevSlideId, transitionType);
    } else if(transitionType === 'Flip') {
        Flip(slideId, prevSlideId);
    } else if(transitionType === 'Flythrough' || transitionType === 'Warp') {
        FlythroughWarp(slideId, prevSlideId, transitionType);
    } else if(transitionType === 'Switch') {
        Switch(slideId, prevSlideId);
    } else if(transitionType === 'Reveal') {
        Reveal(slideId, prevSlideId);
    } else if(transitionType === 'Ferris') {
        Ferris(slideId, prevSlideId);
    } else if(transitionType === 'Honeycomb') {
        Honeycomb(slideId, prevSlideId);
    } else if(transitionType === 'Comb') {
        Comb(slideId, prevSlideId);
    } else {
        $(prevSlideId).hide();
        $(slideId).show();
        PlayTransitionEnd();
    }
}

function PlayTransitionBegin() {
    $("#PrevSlide").prop('disabled',true);
    $("#NextSlide").prop('disabled',true);
    
    ChangeThumbSelection(currentVisiblePage);
}

function PlayTransitionEnd() {
    if(currentVisiblePage != 1) {
        $("#PrevSlide").prop('disabled',false);
    }
    
    if(currentVisiblePage != maxVisiblePage) {
        $("#NextSlide").prop('disabled',false);
    }
}

function ResetAnimationProperties() {
    $(".slide").css("z-index", '');
    $(".slide-content").css('transform', '');
    $(".slide-content").css('position', '');
    $(".slide-content").css('top', '');
}

function Fade(slideId, prevSlideId) {
    if(prevSlideId) {
        StackSlides(slideId, prevSlideId);
    }
    
    var duration = GetDuration(slideId); 
    $(slideId).fadeIn(duration).promise().done(function(){
        $(prevSlideId).hide();
        PlayTransitionEnd();
    });
}

function PushPan(slideId, prevSlideId, transitionType) {
    
    var duration = GetDuration(slideId);
    var direction = $(slideId).data("transitionDirection");
    
    var translateX = 0;
    var translateY = 0;
    
    switch (direction) {
        case 'Right':
            translateX = -frameWidth;
            break;
        case 'Left':
            translateX = frameWidth;
            break;
        case 'Down':
            translateY = -frameHeight;
            break;
        case 'Up':
            translateY = frameHeight;
            break;
        default:
            alert(direction);
            break;
    }
    
    $(slideId).show();
        
    if (transitionType == 'Pan')
        $(slideId).css('opacity', '0');
    $(slideId).css('transform', 'translateX(' + translateX + 'px) translateY(' + translateY + 'px)');
    StackSlides(slideId, prevSlideId);
    
    var timeline_old = anime.timeline({
        targets: prevSlideId,
        autoplay: false,
        easing: 'linear',
        complete: function(anim) {
            
            $(slideId).css('transform', '');
            $(slideId).css('opacity', '1');
            $(prevSlideId).css('transform', '');
            $(prevSlideId).css('opacity', '1');
            $(prevSlideId).hide();
            PlayTransitionEnd();
        }
    });
    
    var timeline_new = anime.timeline({
        targets: slideId,
        autoplay: false,
        easing: 'linear',
    });
    
    timeline_old.add({
        duration: duration,
        translateX: -translateX,
        translateY: -translateY,
        opacity: transitionType == 'Pan' ? 0 : 1,
    });
    
    timeline_new.add({
        duration: duration,
        translateX: 0,
        translateY: 0,
        opacity: 1,
    });
    
    timeline_old.play();
    timeline_new.play();
}

function Pull(slideId, prevSlideId) {    

    var duration = GetDuration(slideId);
    var direction = $(slideId).data("transitionDirection");
    
    var translateX = 0;
    var translateY = 0;
    
    switch (direction) {
        case 'Right':
            translateX = frameWidth;
            break;
        case 'Left':
            translateX = -frameWidth;
            break;
        case 'Down':
            translateY = frameHeight;
            break;
        case 'Up':
            translateY = -frameHeight;
            break;
        case 'LeftDown':
            translateX = -frameWidth;
            translateY = frameHeight;
            break;
        case 'LeftUp':
            translateX = -frameWidth;
            translateY = -frameHeight;
            break;
        case 'RightDown':
            translateX = frameWidth;
            translateY = frameHeight;
            break;
        case 'RightUp':
            translateX = frameWidth;
            translateY = -frameHeight;
            break;
        default:
            alert(direction);
            break;
    }
    
    $(slideId).show();
    StackSlides(prevSlideId, slideId);
    
    var timeline_old = anime.timeline({
        targets: prevSlideId,
        autoplay: false,
        easing: 'linear',
        complete: function(anim) {
            
            $(prevSlideId).css('transform', '');
            StackSlides(slideId, prevSlideId);
            $(prevSlideId).hide();
            PlayTransitionEnd();
        }
    });
    
    timeline_old.add({
        duration: duration,
        translateX: translateX,
        translateY: translateY,
    });
    
    timeline_old.play();
}

function Cover(slideId, prevSlideId) {    

    var duration = GetDuration(slideId);
    var direction = $(slideId).data("transitionDirection");
    
    var translateX = 0;
    var translateY = 0;
    
    switch (direction) {
        case 'Right':
            translateX = -frameWidth;
            break;
        case 'Left':
            translateX = frameWidth;
            break;
        case 'Down':
            translateY = -frameHeight;
            break;
        case 'Up':
            translateY = frameHeight;
            break;
        case 'LeftDown':
            translateX = frameWidth;
            translateY = -frameHeight;
            break;
        case 'LeftUp':
            translateX = frameWidth;
            translateY = frameHeight;
            break;
        case 'RightDown':
            translateX = -frameWidth;
            translateY = -frameHeight;
            break;
        case 'RightUp':
            translateX = -frameWidth;
            translateY = frameHeight;
            break;
        default:
            alert(direction);
            break;
    }
    
    StackSlides(prevSlideId, slideId);
    $(slideId).show();
    $(slideId).css('transform', 'translateX(' + translateX + 'px) translateY(' + translateY + 'px)');
    StackSlides(slideId, prevSlideId);
    
    var timeline_new = anime.timeline({
        targets: slideId,
        autoplay: false,
        easing: 'linear',
        complete: function(anim) {
            
            $(slideId).css('transform', '');
            StackSlides(slideId, prevSlideId);
            $(prevSlideId).hide();
            PlayTransitionEnd();
        }
    });
    
    timeline_new.add({
        duration: duration,
        translateX: 0,
        translateY: 0,
    });
    
    timeline_new.play();
}

function RandomBar(slideId, prevSlideId) {

    var duration = GetDuration(slideId);
    var direction = $(slideId).data("transitionDirection");
    
    
    var bars = GenerateBars(direction == 'Vertical', 5, 70, 1);
    ShuffleArray(bars);

    $(slideId).css('opacity', '0.0');
    $(slideId).show();

    StackSlides(slideId, prevSlideId);
    
    $(slideId).css('clip-path', 'url(#effectsclip)');
    
    var timeline = anime.timeline({
        targets: slideId,
        autoplay: false,
        complete: function(anim) {
            $('#effectsclip').empty();
            $('#svgdiv').html($('#svgdiv').html());
            $(slideId).css('clip-path', '');
            $(prevSlideId).hide();
            PlayTransitionEnd();
        }
    });
      
    
    var finalBarsStartIndex = timeline.id;
    var i, j, k;
    var bigSteps = 1;
    var curStepBegin = 0;
    var eachStepLengh = Math.floor(bars.length / Math.max(bigSteps - 1, 1));
    
    var bigStepOpacityBase, curMinOpacity, curMaxOpacity;
    var curStepLength, curStepBars, maxBarLength;
    
    var finalBars = [];
    
    for (i = 0; i < bigSteps; i++) {
        
        bigStepOpacityBase = (1 / bigSteps) * i;
                
        curStepLength = (i < bigSteps - 1) ? eachStepLengh : bars.length - curStepBegin;
        curStepBars = bars.slice(curStepBegin, curStepBegin + curStepLength);
        maxBarLength = Math.max.apply(Math, $.map(curStepBars, function (el) { return el.length }));
        
        for (j = 0; j < maxBarLength; j++) {
            
            var microStepBars = []
            
            for(k = 0; k < curStepBars.length; k++) {
                if (j < curStepBars[k].length)
                    microStepBars.push(curStepBars[k][j]);
            }
            
            finalBars.push(microStepBars);
            
            curMinOpacity = bigStepOpacityBase + (1 / bigSteps / maxBarLength) * j;
            curMaxOpacity = bigStepOpacityBase + (1 / bigSteps / maxBarLength) * (j + 1);
            
            timeline.add({
                duration: duration / bigSteps / maxBarLength,
                opacity: [curMinOpacity, curMaxOpacity],
                easing: 'linear',
                complete: function(anim) {
                    
                    var ii = anim.id - finalBarsStartIndex - 1;
                    var jj;
                    for (jj = 0; jj < finalBars[ii].length; jj++)
                        $('#effectsclip').append(finalBars[ii][jj]);                    
                    $('#svgdiv').html($('#svgdiv').html());
                    
                }
            });
        }
        
        curStepBegin += eachStepLengh;
    }
    
    timeline.play();
}

function Flash(slideId, prevSlideId) {
    
    var duration = GetDuration(slideId);
    
    $('#flash').show();
    StackSlides('#flash', prevSlideId);
    
    
    var timeline = anime.timeline({
        targets: slideId,
        autoplay: false,
        complete: function(anim) {
            
            $('#flash').hide();
            $(prevSlideId).hide();
            PlayTransitionEnd();
        }
    });
    
    var flashDuration = 300;
    var stepsCount = 100;
    
    var saturate = 2;
    var brightness = 3;
    var opacity = 0.4
    var contrast = 1.5;
    
    var durationStep = (duration - flashDuration) / stepsCount;
    var saturateStep = (saturate - 1) / stepsCount;
    var brightnessStep = (brightness - 1) / stepsCount;
    var contrastStep = (contrast - 1) / stepsCount;
    var opacityStep = (1 - opacity) / stepsCount
    
    timeline.add({
        duration: flashDuration,
        complete: function(anim) {
            
            $(slideId).css('opacity', opacity);
            $(slideId).css('filter', 'saturate(' + saturate + ') brightness(' + brightness + ') contrast(' + contrast + ')');
            $(slideId).show();
            StackSlides(slideId, prevSlideId);
        }
    });
    
    for(var i = 0; i < stepsCount; i++) {
        
        saturate -= saturateStep;
        brightness -= brightnessStep;
        contrast -= contrastStep;
        opacity += opacityStep;
        
        timeline.add({
            duration: durationStep,
            opacity: opacity,
            filter: 'saturate(' + saturate + ') brightness(' + brightness + ') contrast(' + contrast + ')'
        });
    }
    
    timeline.play();
}

function CubeBoxRotateOrbit(slideId, prevSlideId, transitionType) {
    
    var duration = GetDuration(slideId);
    var direction = $(slideId).data("transitionDirection");
    
    if (transitionType == 'Cube' || transitionType == 'Box') {
        $('#blackboard').show();
        StackSlides(prevSlideId, '#blackboard');
    }
    StackSlides(prevSlideId, slideId);
    $(slideId).show();
    
    var timeline_old = anime.timeline({
        targets: prevSlideId,
        autoplay: false,
        easing: 'linear',
        complete: function(anim) {
            
            $(slideId).css('transform', '');
            $(prevSlideId).css('transform', '');
            $(prevSlideId).hide();
            $('#blackboard').hide();
            PlayTransitionEnd();
        }
    });
    
    var timeline_new = anime.timeline({
        targets: slideId,
        autoplay: false,
        easing: 'linear',
    });
    
    
    var perspectiveVal = 3500;
    var translateZVal = -1000;
    var translateXVal = 0;
    var translateXValNew = 0;
    var rotateYVal = 0;
    var translateYVal = 0;
    var translateYValNew = 0;
    var rotateXVal = 0;
    var oppositeDir = (direction == 'Right' || direction == 'Down') ? -1 : 1;
    
    if (transitionType == 'Cube' || transitionType == 'Rotate') {
        if (direction == 'Left' || direction == 'Right') {
            translateXVal = oppositeDir * (frameWidth / 2 + 1000);
            translateXValNew = translateXVal - oppositeDir * 250;
            rotateYVal = oppositeDir * 82;
        }
        else {
            translateYVal = oppositeDir * (frameHeight / 2 + 1000);
            translateYValNew = translateYVal - oppositeDir * 550;
            rotateXVal = oppositeDir * (-85);
        }
    }
    else {
        perspectiveVal = 3000;
        translateZVal = -2050;
        
        if (direction == 'Left' || direction == 'Right') {
            translateXVal = oppositeDir * (frameWidth / 2 - 1500);
            translateXValNew = translateXVal + oppositeDir * 250;
            rotateYVal = oppositeDir * (-98);
        }
        else {
            translateZVal = -1900;
            translateYVal = oppositeDir * (frameHeight / 2 - 1500);
            translateYValNew = translateYVal + oppositeDir * 550;
            rotateXVal = oppositeDir * (95);
        }
    }
    
    $(prevSlideId).css('transform', 'perspective(' + perspectiveVal + 'px)');
    $(slideId).css('transform', 'perspective(' + perspectiveVal + 'px) translateX(' + translateXValNew + 'px) translateY(' + translateYValNew + 'px) rotateX(' + rotateXVal + 'deg) rotateY(' + rotateYVal + 'deg) translateZ(' + (translateZVal) + 'px)');
    
    timeline_old.add({
        duration: duration,
        translateX: [0, -translateXVal],
        translateY: [0, -translateYVal],
        rotateX: -rotateXVal,
        rotateY: -rotateYVal,
        translateZ: translateZVal,
    });    
        
    timeline_new.add({
        duration: duration,
        translateX: 0,
        rotateY: 0,
        translateY: 0,
        rotateX: 0,
        translateZ: 0
    });
    
    timeline_old.play();
    timeline_new.play();
}

function GalleryConveyor(slideId, prevSlideId, transitionType) {
    
    var duration = GetDuration(slideId);
    var direction = $(slideId).data("transitionDirection");
    
    if (transitionType == 'Gallery') {
        $('#blackboard').show();
        StackSlides(prevSlideId, '#blackboard');
    }
    StackSlides(prevSlideId, slideId);
    $(slideId).show();
    
    var timeline_old = anime.timeline({
        targets: prevSlideId,
        autoplay: false,
        easing: 'easeInOutCirc',
        complete: function(anim) {
            
            $(slideId).css('transform', '');
            $(prevSlideId).css('transform', '');
            $(prevSlideId).hide();
            $('#blackboard').hide();
            PlayTransitionEnd();
        }
    });
    
    var timeline_new = anime.timeline({
        targets: slideId,
        autoplay: false,
        easing: 'easeInOutCirc',
    });
    
    
    var perspectiveVal = 3500;
    var translateZVal = -400;
    
    var oppositeDir = (direction == 'Right') ? -1 : 1;
    var translateZValFix = 40;
    
    var translateXVal = oppositeDir * 100;
    var translateXValNew = oppositeDir * (frameWidth + translateZValFix);
    var rotateYVal = oppositeDir * 30;
    
    $(prevSlideId).css('transform', 'perspective(' + perspectiveVal + 'px)');
    $(slideId).css('transform', 'perspective(' + perspectiveVal + 'px) translateX(' + translateXValNew + 'px) translateZ(' + translateZValFix + 'px) rotateY(' + (-rotateYVal) + 'deg)');
    
    timeline_old.add({
        duration: duration / 3,
        translateX: -translateXVal,
        rotateY: -rotateYVal,
        translateZ: translateZVal,
    });
    
    timeline_old.add({
        duration: duration / 3,
        translateX: oppositeDir * (-frameWidth) * 1.5,
        translateZ: translateZVal * 3.05,
    });
    
    timeline_old.add({
        duration: duration / 3,
        translateX: oppositeDir * (-frameWidth),
        rotateY: 0,
        translateZ: 0,
    });
    
    timeline_new.add({
        duration: duration / 3, // empty action
    });
    
    timeline_new.add({
        duration: duration / 3,
        translateX: translateXVal - oppositeDir * translateZValFix,
        translateZ: translateZVal * 1.55,
    });
    
    timeline_new.add({
        duration: duration / 3,
        translateX: 0,
        rotateY: 0,
        translateZ: 0,
    });
    
    timeline_old.play();
    timeline_new.play();
}

function Flip(slideId, prevSlideId,) {
    
    var duration = GetDuration(slideId);
    var direction = $(slideId).data("transitionDirection");
    
    $('#blackboard').show();
    StackSlides(prevSlideId, '#blackboard');
    StackSlides(prevSlideId, slideId);
    $(slideId).show();
    
    var timeline_old = anime.timeline({
        targets: prevSlideId,
        autoplay: false,
        easing: 'linear',
        complete: function(anim) {
            timeline_new.play();
        }
    });
    
    var timeline_new = anime.timeline({
        targets: slideId,
        autoplay: false,
        easing: 'linear',
        complete: function(anim) {
            
            $(slideId).css('transform', '');
            $(prevSlideId).css('transform', '');
            $(prevSlideId).hide();
            $('#blackboard').hide();
            PlayTransitionEnd();
        }
    });
    
    
    var perspectiveVal = 3500;
    var oppositeDir = (direction == 'Left') ? -1 : 1;
    var rotateYVal = oppositeDir * 90;
    
    $(prevSlideId).css('transform', 'perspective(' + perspectiveVal + 'px)');
    $(slideId).css('transform', 'perspective(' + perspectiveVal + 'px) rotateY(' + (-rotateYVal) + 'deg)');
    
    timeline_old.add({
        duration: duration / 2,
        rotateY: rotateYVal,
    });
    
    timeline_new.add({
        duration: duration / 2,
        rotateY: 0,
    });

    timeline_old.play();
}

function Dissolve(slideId, prevSlideId) {
    
    var duration = GetDuration(slideId);
    var stepsCount = 50;
    
    var tiles = GenerateDissolvePolygons();
    ShuffleArray(tiles);
    
    $(slideId).css('opacity', '0.0');
    $(slideId).show();

    StackSlides(slideId, prevSlideId);
    
    $(slideId).css('clip-path', 'url(#effectsclip)');
    
    var timeline = anime.timeline({
        targets: slideId,
        autoplay: false,
        complete: function(anim) {
            $('#effectsclip').empty();
            $('#svgdiv').html($('#svgdiv').html());
            $(slideId).css('clip-path', '');
            $(prevSlideId).hide();
            PlayTransitionEnd();
        }
    });

    var indexBase = timeline.id;
    var opacityStep, curOpacity;
    var tilesCount = Math.ceil(tiles.length / stepsCount);
    
    curOpacity = 0;
    opacityStep = (1 - curOpacity) / stepsCount;
    
    for (var i = 0; i < stepsCount; i++) {
        
        timeline.add({
            duration: duration / stepsCount,
            opacity: [curOpacity, curOpacity + opacityStep],
            easing: 'linear',
            complete: function(anim) {
                
                var j = anim.id - indexBase - 1;
                
                for (var k = tilesCount * j; k < tilesCount * (j + 1) && k < tiles.length; k++)
                    $('#effectsclip').append(tiles[k]);
                
                $('#svgdiv').html($('#svgdiv').html());
            }
        });
        
        curOpacity += opacityStep;
    }
    
    timeline.play();
}

function FlythroughWarp(slideId, prevSlideId, transitionType) {
    
    var duration = GetDuration(slideId);
    var direction = $(slideId).data("transitionDirection");
    var hasBounce = $(slideId).data("transitionExtra") == 'HasBounce';
    var animeDirection = direction == 'In' ? 'normal' : 'reverse';
    
    if (transitionType == 'Warp' && direction == 'In') {
        $('#blackboard').show();
        StackSlides(prevSlideId, '#blackboard');
    }
    if (direction == 'In')
        StackSlides(prevSlideId, slideId);
    else
        StackSlides(slideId, prevSlideId);
    
    $(slideId).show();
    
    var timeline_outside = anime.timeline({
        targets: direction == 'In' ? prevSlideId : slideId,
        autoplay: false,
        easing: (direction == 'Out' && hasBounce) ? 'easeInBounce' : 'easeInOutExpo',
        direction: animeDirection,
        complete: function(anim) {
            
            $(slideId).css('transform', '');
            $(slideId).css('opacity', '1');
            $(prevSlideId).css('transform', '');
            $(prevSlideId).css('opacity', '1');
            $(prevSlideId).hide();
            $('#blackboard').hide();
            PlayTransitionEnd();
        }
    });
    
    var timeline_inside = anime.timeline({
        targets: direction == 'In' ? slideId: prevSlideId,
        autoplay: false,
        direction: animeDirection,
        easing: (direction == 'In' && hasBounce) ? 'easeOutBounce' : 'easeInOutExpo',
    });
    
    timeline_outside.add({
        duration: duration,
        scale: [1, 4],
        opacity: [1, 0],
    });    
        
    timeline_inside.add({
        duration: duration,
        scale: [0.5, 1],
    });
    
    timeline_outside.play();
    timeline_inside.play();
}

function Switch(slideId, prevSlideId) {
    
    var duration = GetDuration(slideId);
    var direction = $(slideId).data("transitionDirection");
    
    $('#blackboard').show();
    StackSlides(prevSlideId, '#blackboard');
    StackSlides(prevSlideId, slideId);
    
    var timeline_old = anime.timeline({
        targets: prevSlideId,
        autoplay: false,
        easing: 'linear',
        complete: function(anim) {
            
            $(slideId).css('transform', '');
            $(prevSlideId).css('transform', '');
            $(prevSlideId).hide();
            $('#blackboard').hide();
            PlayTransitionEnd();
        }
    });
    
    var timeline_new = anime.timeline({
        targets: slideId,
        autoplay: false,
        easing: 'linear',
    });
    
    
    var perspectiveVal = 3000;
    var translateZVal = -800;
    
    var oppositeDir = (direction == 'Right') ? -1 : 1;
    
    var translateXVal = oppositeDir * 100;
    var translateXValNew = translateXVal + oppositeDir * (-frameWidth / 2);
    var rotateYVal = oppositeDir * 40;
    
    $(prevSlideId).css('transform', 'perspective(' + perspectiveVal + 'px)');
    $(slideId).css('transform', 'perspective(' + perspectiveVal + 'px) translateX(' + (-translateXValNew / 2) + 'px) translateZ(' + (translateZVal / 2) + 'px) rotateY(' + (-rotateYVal / 2) + 'deg)');
    $(slideId).show();
    
    timeline_old.add({
        duration: duration / 2,
        translateX: [0, translateXVal],
        rotateY: [0, rotateYVal],
        translateZ: [0, translateZVal],
        complete: function(anim) {
            
            StackSlides(slideId, prevSlideId);
        }
    });
    
    timeline_old.add({
        duration: duration / 2,
        translateX: [translateXVal, translateXVal / 2],
        rotateY: [rotateYVal, rotateYVal / 2],
        translateZ: [translateZVal, translateZVal / 2],
    });
    
    
    timeline_new.add({
        duration: duration / 2,
        translateX: [-translateXValNew / 2, -translateXValNew],
        rotateY: [-rotateYVal / 2, -rotateYVal],
        translateZ: [translateZVal / 2, translateZVal],
    });
    
    
    timeline_new.add({
        duration: duration / 2,
        translateX: [-translateXValNew, 0],
        rotateY: [-rotateYVal, 0],
        translateZ: [translateZVal, 0]
    });
    
    timeline_old.play();
    timeline_new.play();
}

function Reveal(slideId, prevSlideId) {
    
    var duration = GetDuration(slideId);
    var direction = $(slideId).data("transitionDirection");
    var throughBlack = $(slideId).data("transitionExtra") == 'ThroughBlack';
    
    if (throughBlack) {
        $('#blackboard').show();
        StackSlides(prevSlideId, '#blackboard');
    }
    StackSlides(prevSlideId, slideId);
    
    var timeline_old = anime.timeline({
        targets: prevSlideId,
        autoplay: false,
        easing: 'linear',
    });
    
    var timeline_new = anime.timeline({
        targets: slideId,
        autoplay: false,
        easing: 'linear',
        complete: function(anim) {
            $(slideId).css('transform', '');
            $(prevSlideId).css('opacity', '1');
            $(prevSlideId).css('transform', '');
            $(prevSlideId).hide();
            $('#blackboard').hide();
            PlayTransitionEnd();
        }
    });
    
    
    var perspectiveVal = 3000;
    var translateZVal = 100;
    var oppositeDir = (direction == 'Right') ? -1 : 1;
    
    var translateXVal = oppositeDir * -40;
    var translateXValNew = 10;
    
    
    $(prevSlideId).css('transform', 'perspective(' + perspectiveVal + 'px)');
    $(slideId).css('transform', 'perspective(' + perspectiveVal + 'px) translateX(' + translateXValNew + 'px) translateZ(' + translateZVal + 'px)');
    $(slideId).css('opacity', '0');
    $(slideId).show();
    
    
    timeline_old.add({
        duration: duration / 2,
        translateX: [0, translateXVal],
        translateZ: [0, translateZVal * 2],
        opacity: [1, 0],
        complete: function(anim) {
            timeline_new.play();
        }
    });
    
    
    timeline_new.add({
        duration: duration / 2,
        opacity: [0, 1],
        translateX: [translateXValNew, 0],
        translateZ: [translateZVal, 0],
    });
    
    timeline_old.play();
}

function Ferris(slideId, prevSlideId) {
    
    var duration = GetDuration(slideId);
    var direction = $(slideId).data("transitionDirection");

    StackSlides(prevSlideId, slideId);
    
    var timeline_old = anime.timeline({
        targets: prevSlideId,
        autoplay: false,
        easing: 'linear',
    });
    
    var timeline_new = anime.timeline({
        targets: slideId,
        autoplay: false,
        easing: 'linear',
        complete: function(anim) {
            $(slideId).css('transform', '');
            $(prevSlideId).css('opacity', '1');
            $(prevSlideId).css('transform', '');
            $(prevSlideId).hide();
            $('#blackboard').hide();
            PlayTransitionEnd();
        }
    });
    
    
    var perspectiveVal = 3000;
    var translateZVal = -500;
    var oppositeDir = (direction == 'Right') ? -1 : 1;
    
    var translateXVal = oppositeDir * -150;
    var translateXValNew = 10;
    var translateYVal = 600;
    var rotateYVal = oppositeDir * 20;
    var rotateZVal = oppositeDir * 10;
    
    
    $(prevSlideId).css('transform', 'perspective(' + perspectiveVal + 'px)');
    $(slideId).css('transform', 'perspective(' + perspectiveVal + 'px) translateX(' + translateXVal + 'px) translateY(' + (-translateYVal) + 'px) translateZ(' + translateZVal + 'px) rotateY(' + (-rotateYVal) + 'deg) rotateZ(' + (-rotateZVal) + 'deg)');
    $(slideId).css('opacity', '0');
    $(slideId).show();
    
    
    timeline_old.add({
        duration: duration / 2,
        translateX: [0, translateXVal],
        translateY: [0, translateYVal],
        translateZ: [0, translateZVal],
        rotateY: [0, rotateYVal],
        rotateZ: [0, rotateZVal],
        opacity: [1, 0],
        complete: function(anim) {
            timeline_new.play();
        }
    });
    
    
    timeline_new.add({
        duration: duration / 2,
        translateX: [translateXVal, 0],
        translateY: [-translateYVal, 0],
        translateZ: [translateZVal, 0],
        rotateY: [-rotateYVal, 0],
        rotateZ: [-rotateZVal, 0],
        opacity: [0, 1],
    });
    
    timeline_old.play();
}

function Honeycomb(slideId, prevSlideId) {
    
    var duration = GetDuration(slideId) * 2;
    
    var blackboardClone = null;
    if (prevSlideId == null)
    {
        blackboardClone = $('#blackboard').clone();
        blackboardClone.attr('id', 'blackboardClone');
        blackboardClone.insertBefore('#svgdiv');
        prevSlideId = '#blackboardClone';
    }

    StackSlides(prevSlideId, '#blackboard');
    $('#blackboard').show();
    
    var polys_old = GenerateHoneycombPolygons(true);
    var polys_new = GenerateHoneycombPolygons(false);
    $('#effectsclip').append(polys_old);
    $('#svgdiv').html($('#svgdiv').html());
    $(prevSlideId).css('clip-path', 'url(#effectsclip)');
    
    var oldScaleVal = 10;
    var oldTranslateXVal = -10;
    var oldTranslateYVal = -40;
    var oldRotateZVal = -60;
    
    var newScaleVal = 0.3;
    var newRotateZVal = 70;
    
    var timeline_old = anime.timeline({
        targets: prevSlideId,
        autoplay: false,
        easing: 'easeInQuad',
        complete: function(anim) {
            $('#effectsclip').empty();
            $('#svgdiv').html($('#svgdiv').html());
            $(prevSlideId).css('clip-path', '');
            $(prevSlideId).css('transform', '');
            $(prevSlideId).hide();
            if (blackboardClone != null)
                blackboardClone.remove();
        }
    });
    
    var timeline_new = anime.timeline({
        targets: slideId,
        autoplay: false,
        easing: 'easeOutSine',
        complete: function(anim) {
            $('#effectsclip1').empty();
            $('#svgdiv').html($('#svgdiv').html());
            $(slideId).css('clip-path', '');
            PlayTransitionEnd();
        }
    });
    
    
    timeline_old.add({
        duration: duration,
        scaleX: oldScaleVal,
        scaleY: oldScaleVal,
        translateX: oldTranslateXVal,
        translateY: oldTranslateYVal,
        rotateZ: oldRotateZVal
    });
    
    timeline_new.add({
        duration: duration / 1.5,
        scaleX: [newScaleVal, 1],
        scaleY: [newScaleVal, 1],
        rotateZ: [newRotateZVal, 0]
    });
    
    
    var timeline_remove = anime.timeline({
        autoplay: false,
    });
    
    for (var i = 0; i < polys_old.length; i++) {
        timeline_remove.add({
            duration: duration / 1.5 / polys_old.length,
            complete: function(anim) {
                $('#effectsclip').find('polygon:first').remove();
                $('#svgdiv').html($('#svgdiv').html());
            }
        });
    }
    
    
    var timeline_add = anime.timeline({
        autoplay: false,
    });
    
    var indexBase = timeline_add.id;
    
    for (var i = 0; i < polys_new.length; i++) {
        timeline_add.add({
            duration: duration / 1.5 / polys_new.length,
            complete: function(anim) {
                var j = anim.id - indexBase - 1;
                $('#effectsclip1').append(polys_new[j]);
                $('#svgdiv').html($('#svgdiv').html());
            }
        });
    }
    
    var timeline_delay = anime.timeline({
        autoplay: false,
    });
    
    timeline_delay.add({
        duration: duration / 3,
        complete: function(anim) {
            
            $('#blackboard').css("z-index", 1);
            $(slideId).css("z-index", 2);
            $(prevSlideId).css("z-index", 3);
    
            $(slideId).css('transform', 'scaleX(' + (newScaleVal) + ') scaleY(' + (newScaleVal) + ') rotateZ(' + (newRotateZVal) + 'deg)');
            $(slideId).css('clip-path', 'url(#effectsclip1)');
            $(slideId).show();
            timeline_new.play();
            timeline_add.play();
        }
    });
    
    
    timeline_old.play();
    timeline_delay.play();
    timeline_remove.play();
}

function Comb(slideId, prevSlideId) {
    
    var duration = GetDuration(slideId);
    var direction = $(slideId).data("transitionDirection");
    var horizontal = direction == 'Horizontal';
    
    if (prevSlideId == null) 
        prevSlideId = '#blackboard';

    StackSlides(prevSlideId, slideId);
    $(slideId).show();
    
    var timeline1 = CreateCloneAndTimeline(prevSlideId, 1);
    var timeline2 = CreateCloneAndTimeline(prevSlideId, 2);
    var timeline3 = CreateCloneAndTimeline(prevSlideId, 3);
    var timeline4 = CreateCloneAndTimeline(prevSlideId, 4);
    var timeline5 = CreateCloneAndTimeline(prevSlideId, 5);
    var timeline6 = CreateCloneAndTimeline(prevSlideId, 6);
    var timeline7 = CreateCloneAndTimeline(prevSlideId, 7);
    var timeline8 = CreateCloneAndTimeline(prevSlideId, 8);
    
    $(prevSlideId).hide();
    
    var polys = GenerateCombPolygons(direction);
    $('#effectsclip1').append(polys[0]);
    $('#effectsclip2').append(polys[1]);
    $('#effectsclip3').append(polys[2]);
    $('#effectsclip4').append(polys[3]);
    $('#effectsclip5').append(polys[4]);
    $('#effectsclip6').append(polys[5]);
    $('#effectsclip7').append(polys[6]);
    $('#effectsclip8').append(polys[7]);
    
    $('#svgdiv').html($('#svgdiv').html());
    
    var translateXVal = horizontal ? frameWidth + 10 : 0;
    var translateYVal = horizontal ? 0 : frameHeight + 10;
    var delayVal = horizontal ? 150 : 0;
    
    timeline1.add({
        duration: duration,
        translateX: translateXVal,
        translateY: translateYVal
    });
    
    timeline2.add({
        duration: duration,
        translateX: -translateXVal,
        translateY: -translateYVal
    });
    
    timeline3.add({
        duration: duration,
        delay: delayVal,
        translateX: translateXVal,
        translateY: translateYVal
    });
    
    timeline4.add({
        duration: duration,
        delay: delayVal,
        translateX: -translateXVal,
        translateY: -translateYVal
    });
    
    timeline5.add({
        duration: duration,
        delay: delayVal * 2,
        translateX: translateXVal,
        translateY: translateYVal
    });
    
    timeline6.add({
        duration: duration,
        delay: delayVal * 2,
        translateX: -translateXVal,
        translateY: -translateYVal
    });
    
    timeline7.add({
        duration: duration,
        delay: delayVal * 3,
        translateX: translateXVal,
        translateY: translateYVal
    });
    
    timeline8.add({
        duration: duration,
        delay: delayVal * 3,
        translateX: -translateXVal,
        translateY: -translateYVal,
        complete: function(anim) {
            $('#svgdiv').html($('#svgdiv').html());
            PlayTransitionEnd();
        }
    });
    
    timeline1.play();
    timeline2.play();
    timeline3.play();
    timeline4.play();
    timeline5.play();
    timeline6.play();
    timeline7.play();
    timeline8.play();
}

function CreateCloneAndTimeline(slideId, cloneNumber) {
    
    var cloneId = '#slideclone' + cloneNumber;
    var effectsId = '#effectsclip' + cloneNumber;
    var clone = $(slideId).clone();
    clone.attr('id', 'slideclone' + cloneNumber);
    clone.css('clip-path', 'url(' + effectsId + ')');
    clone.insertBefore('#svgdiv');
    clone.show();
    
    var timeline_clone = anime.timeline({
        targets: cloneId,
        autoplay: false,
        easing: 'easeInCubic',
        complete: function(anim) {
            
            $(effectsId).empty();
            $(cloneId).remove();
        }
    });
    
    return timeline_clone;
}


function Wheel(slideId, prevSlideId, wheelType) {
    
    var stepsCount = 150;
    var sectors = GenerateWheelSectors(wheelType, stepsCount);
    
    AnimatePolygons(slideId, prevSlideId, sectors);
}

function Shape(slideId, prevSlideId, shapeType) {
    
    var stepsCount = 150;
    
    var polys = [];
    if (shapeType == 'Circle')
        polys = GenerateCircles(stepsCount);
    else
        polys = GenerateShapePolygons(stepsCount, shapeType);
    
    AnimatePolygons(slideId, prevSlideId, polys);
}

function Split(slideId, prevSlideId) {
    
    var stepsCount = 150;
    
    var out = $(slideId).data("transitionDirection") == 'Out';
    var horizontal = $(slideId).data("transitionExtra") == 'Horizontal';
    
    var polys = [];
    polys = GenerateSplitPolygons(stepsCount, horizontal, out);
    
    AnimatePolygons(slideId, prevSlideId, polys);
}

function Wipe(slideId, prevSlideId) {

    var stepsCount = 150;
    
    var direction = $(slideId).data("transitionDirection");
    
    var polys = [];
    polys = GenerateWipePolygons(stepsCount, direction);
    
    AnimatePolygons(slideId, prevSlideId, polys);
}

function AnimatePolygons(slideId, prevSlideId, polys) {
    
    var duration = GetDuration(slideId);
    var stepsCount = polys.length;
    var curOpacity = 0.8;
    
    $(slideId).css('opacity', curOpacity);
    $(slideId).show();

    StackSlides(slideId, prevSlideId);
    
    $(slideId).css('clip-path', 'url(#effectsclip)');
    
    var timeline = anime.timeline({
        targets: slideId,
        autoplay: false,
        complete: function(anim) {
            $('#effectsclip').empty();
            $('#svgdiv').html($('#svgdiv').html());
            $(slideId).css('clip-path', '');
            $(prevSlideId).hide();
            PlayTransitionEnd();
        }
    });

    var indexBase = timeline.id;
    var opacityStep = (1 - curOpacity) / stepsCount;
    
    for (var i = 0; i < stepsCount; i++) {
        
        timeline.add({
            duration: duration / stepsCount,
            opacity: [curOpacity, curOpacity + opacityStep],
            easing: 'linear',
            complete: function(anim) {
                
                var j = anim.id - indexBase - 1;
                $('#effectsclip').append(polys[j]);
                $('#svgdiv').html($('#svgdiv').html());
            }
        });
        
        curOpacity += opacityStep;
    }
    
    timeline.play();
}

function GenerateBars(vertical, minWidth, maxWidth, gradationWidth) {
    
    var result = polygonsCache.Bars;
    if (!result) {
        
        result = [];
        var curPosition = 0;
        var limit = vertical ? frameWidth : frameHeight;
        var widths = [];
            
        while (curPosition < limit) {
            
            var nextBarWidth = Math.floor((Math.random() * (maxWidth - minWidth)) + minWidth);
            if (curPosition + nextBarWidth > limit)
                nextBarWidth = limit - curPosition;
            
            if (nextBarWidth > 1)
                widths.push(nextBarWidth);
            
            curPosition += nextBarWidth;
        };
            
    
        var prevStart = 0;
        for(var i = 0; i < widths.length; i++) {
            
            var curBarPolygons = [];
            var center = Math.floor((widths[i] + prevStart * 2) / 2);
            
            var left = center - gradationWidth;
            var right = center;
            while (left >= prevStart || right < prevStart + widths[i]) {
                
                if (left >= prevStart)
                    curBarPolygons.push(GenerateBarPolygon(vertical, left, gradationWidth));
                if (right < prevStart + widths[i])
                    curBarPolygons.push(GenerateBarPolygon(vertical, right, gradationWidth));
                
                left -= gradationWidth;
                right += gradationWidth;
            }
            
            result.push(curBarPolygons);
            
            prevStart += widths[i];
        }
        
        polygonsCache.Bars = result;
    }
    
    return result;
}

function GenerateBarPolygon(vertical, start, width) { 
    if (vertical)
        return `<polygon points="${start},${0} ${start + width},${0} ${start + width},${frameHeight} ${start},${frameHeight}"/>`;
    else
        return `<polygon points="${0},${start} ${frameWidth},${start} ${frameWidth},${start + width} ${0},${start + width}"/>`;
}

function GenerateDissolvePolygons() {
    
    var result = polygonsCache.Dissolve;
    if (!result) {
        
        result = [];
        var xStep = frameWidth / 54;
        var yStep = frameHeight / 42;
        var eps = 0.5;
        
        for(var i = 0; i < 54; i++) {
            for(var j = 0; j < 42; j++) {
            
                var x = frameWidth / 54 * i;
                var y = frameHeight / 42 * j;
                
                result.push(`<polygon points="${x - eps},${y - eps} ${x + xStep + eps},${y - eps} ${x + xStep + eps},${y + yStep + eps} ${x - eps},${y + yStep + eps}"/>`);
            }
        }
        
        polygonsCache.Dissolve = result;
    }
        
    return result;
}

function GenerateShapePolygons(stepsCount, shapeType) {
    
    var result = polygonsCache[shapeType];
    if (!result) {
        
        result = [];
        var centerX = frameWidth / 2;
        var centerY = frameHeight / 2;
        
        var xStep = frameWidth / stepsCount / 2;
        var yStep = frameHeight / stepsCount / 2;
        if (shapeType == 'Diamond') {
            xStep *= 2;
            yStep *= 2;
        }
        
        var x, y;
        
        for(var i = 0; i < stepsCount; i++) {
            
            x = xStep * i;
            y = yStep * i;
            
            if (shapeType == 'ZoomOut') {
                x += xStep;
                y += yStep;
            }
            
            if (shapeType == 'Plus')
                result.push(`<polygon points="${0},${centerY - y} ${centerX - x},${centerY - y} ${centerX - x},${0} ${centerX + x},${0} ${centerX + x},${centerY - y} ${frameWidth},${centerY - y} ${frameWidth},${centerY + y} ${centerX + x},${centerY + y} ${centerX + x},${frameHeight} ${centerX - x},${frameHeight} ${centerX - x},${centerY + y} ${0},${centerY + y}"/>`);
            else if (shapeType == 'Diamond')
                result.push(`<polygon points="${centerX - x},${centerY} ${centerX},${centerY - y} ${centerX + x},${centerY} ${centerX},${centerY + y}"/>`);
            else if (shapeType == 'ZoomIn')
                result.push(`<polygon points="${0},${0} ${frameWidth},${0} ${frameWidth},${y} ${x},${y} ${x},${frameHeight - y} ${frameWidth - x},${frameHeight - y} ${frameWidth - x},${y} ${frameWidth},${y} ${frameWidth},${frameHeight} ${0},${frameHeight}"/>`);
            else if (shapeType == 'ZoomOut')
                result.push(`<polygon points="${centerX - x},${centerY - y} ${centerX + x},${centerY - y} ${centerX + x},${centerY + y} ${centerX - x},${centerY + y}"/>`);
        }
    
        polygonsCache[shapeType] = result;
    }
    
    return result;
}

function GenerateSplitPolygons(stepsCount, horizontal, out) {
    
    var cacheKey = 'split' + (horizontal ? 'Hor' : 'Vert') + (out ? 'Out' : 'In');
    var result = polygonsCache[cacheKey];
    if (!result) {
        
        result = [];
        var centerX = frameWidth / 2;
        var centerY = frameHeight / 2;
        
        var xStep = frameWidth / stepsCount / 2;
        var yStep = frameHeight / stepsCount / 2;
        
        var x, y;
        
        for(var i = 0; i < stepsCount; i++) {
            
            x = xStep * i;
            y = yStep * i;
            
            if (horizontal) {
                if (out)
                    result.push(`<polygon points="${0},${centerY - y} ${frameWidth},${centerY - y} ${frameWidth},${centerY + y} ${0},${centerY + y}"/>`);
                else
                    result.push(`<polygon points="${0},${0} ${frameWidth},${0} ${frameWidth},${y} ${0},${y} ${0},${frameHeight - y} ${frameWidth},${frameHeight - y} ${frameWidth},${y} ${frameWidth},${y} ${frameWidth},${frameHeight} ${0},${frameHeight}"/>`);
            }
            else {
                if (out)
                    result.push(`<polygon points="${centerX - x},${0} ${centerX + x},${0} ${centerX + x},${frameHeight} ${centerX - x},${frameHeight}"/>`);
                else
                    result.push(`<polygon points="${0},${0} ${0},${frameHeight} ${x},${frameHeight} ${x},${0} ${frameWidth - x},${0} ${frameWidth - x},${frameHeight} ${frameWidth},${frameHeight} ${frameWidth},${0} ${frameWidth - x},${0} ${0},${0}"/>`);
            }
        }
    
        polygonsCache[cacheKey] = result;
    }
    
    return result;
}

function GenerateWipePolygons(stepsCount, direction) {
    
    var cacheKey = 'wipe' + direction;
    var result = polygonsCache[cacheKey];
    var horizontal = direction == 'Up' || direction == 'Down';
    
    if (!result) {
        
        result = [];
        
        var xStep = frameWidth / stepsCount;
        var yStep = frameHeight / stepsCount;
        var oppositeDir = (direction == 'Left' || direction == 'Up') ? -1 : 1;
        
        var x = direction == 'Left' ? frameWidth : -xStep;
        var y = direction == 'Up' ? frameHeight : -yStep;
        var eps = 0.8;
        
        for(var i = 0; i < stepsCount; i++) {
            
            x += oppositeDir * xStep;
            y += oppositeDir * yStep;
            
            if (horizontal)
                result.push(`<polygon points="${0},${y} ${frameWidth},${y} ${frameWidth},${y + yStep + eps} ${0},${y + yStep + eps}"/>`);
            else
                result.push(`<polygon points="${x},${0} ${x + xStep + eps},${0} ${x + xStep + eps},${frameHeight} ${x},${frameHeight}"/>`);
        }
    
        polygonsCache[cacheKey] = result;
    }
    
    return result;
}

function GenerateHoneycombPolygons(fullTile) {
    
    var cacheKey = 'honeycomb' + fullTile;
    var result = polygonsCache[cacheKey];
    
    if (!result) {
        
        result = [];
        
        var sideLength = 50;
        var gapLength = 10;
        var angle = 75;
        
        var sinCoef = Math.sin(DegreesToRadians(angle));
        var cosCoef = Math.cos(DegreesToRadians(angle));
        var even = true;
        var yShift = 0;
        
        var startX = fullTile ? -sideLength : sideLength;
        var startY = fullTile ? -sideLength : sideLength * cosCoef + sideLength + gapLength * cosCoef * 3 + 10;
        var maxX = fullTile ? frameWidth + sideLength : frameWidth - sideLength;
        var maxY = fullTile ? frameHeight : frameHeight - sideLength * 2;
        
        for (var x = startX; x < maxX; x += sideLength * sinCoef + gapLength * sinCoef / 2)
        {
            for (var y = startY - yShift; y < maxY; y += 2 * (sideLength + sideLength * cosCoef + gapLength * cosCoef * 3)) 
            {
                var x1 = x;
                var y1 = y;
                var x2 = x + sideLength * sinCoef;
                var y2 = y + sideLength * cosCoef;
                var x3 = x2;
                var y3 = y2 + sideLength;
                var x4 = x;
                var y4 = y3 + sideLength * cosCoef;
                var x5 = x - sideLength * sinCoef;
                var y5 = y3;
                var x6 = x5;
                var y6 = y2;
                
                result.push(`<polygon points="${x1},${y1} ${x2},${y2} ${x3},${y3} ${x4},${y4} ${x5},${y5} ${x6},${y6}"/>`);
            }
            
            even = !even;
            yShift = even ? 0 : sideLength * cosCoef + sideLength + gapLength * cosCoef * 3;
        }
        
        ShuffleArray(result);
        
        polygonsCache[cacheKey] = result;
    }
    
    return result;
}

function GenerateCombPolygons(direction) {
    
    var cacheKey = 'comb' + direction;
    var result = polygonsCache[cacheKey];
    var horizontal = direction == 'Horizontal';
    
    if (!result) {
        
        result = [];
        var eps = 1;
        
        if (horizontal) {
            var heightStep = frameHeight / 8 + eps;
            for(var i = 0; i < 8; i++)
                result.push(`<polygon points="${0},${heightStep * i - eps} ${frameWidth + 5},${heightStep * i - eps} ${frameWidth + 5},${heightStep * (i + 1) + eps} ${0},${heightStep * (i + 1) + eps}"/>`);
            result = result.reverse();
        }
        else {
            var widthStep = frameWidth / 8;
            for(var i = 0; i < 8; i++)
                result.push(`<polygon points="${widthStep * i - eps},${0} ${widthStep * i - eps},${frameHeight} ${widthStep * (i + 1) + eps},${frameHeight} ${widthStep * (i + 1) + eps},${0}"/>`);
        }
        
        polygonsCache[cacheKey] = result;
    }
    
    return result;
}

function GenerateWheelSectors(wheelType, stepsCount) {
        
    var result = polygonsCache[wheelType];
    if (!result) {
        
        result = [];
        var angleStep = 360 / stepsCount;
        var currentAngle = 0;
        var radius = Math.max(frameWidth, frameHeight) + 10;
        var centerX = frameWidth / 2;
        var centerY = frameHeight / 2;
        var angleFix = wheelType == 'WheelReverse' ? -2 : 2;
        
        var opts = {
            cx: centerX,
            cy: centerY,
            radius: radius,
            start_angle: angleFix ,
            end_angle: wheelType == 'WheelReverse' ? -angleStep: angleStep
        };
        
        var start, end, largeArcFlag;
        
        for (var i = 0; i < stepsCount && opts.end_angle + angleFix < 362; i++) {
            
            start = PolarToCartesian(opts.cx, opts.cy, opts.radius, opts.end_angle + angleFix);
            end = PolarToCartesian(opts.cx, opts.cy, opts.radius, opts.start_angle - angleFix);
            largeArcFlag = opts.end_angle - opts.start_angle <= 180 ? "0" : "1";
            
            var pathData = [
                "M", start.x, start.y,
                "A", opts.radius, opts.radius, 0, largeArcFlag, 0, end.x, end.y,
                "L", opts.cx, opts.cy,
                "Z"
            ].join(" ");
            
            if (wheelType == 'WheelReverse') {
                opts.start_angle -= angleStep;
                opts.end_angle -= angleStep;
            }
            else {
                opts.start_angle += angleStep;
                opts.end_angle += angleStep;
            }
            
            result.push('<path d="' + pathData + '"/>');
        }
        
        if (wheelType == 'Wedge') {
            var resultRearranged = [];
            for (var i = 0; i < stepsCount / 2; i++) {
                resultRearranged.push(result[i]);
                resultRearranged.push(result[result.length - i]);
            }
            result = resultRearranged;
        }
        
        polygonsCache[wheelType] = result;    
    }
    
    return result;
}

function GenerateCircles(stepsCount) {
    
    var result = polygonsCache.Circles;
    if (!result) {
    
        result = [];
        var centerX = frameWidth / 2;
        var centerY = frameHeight / 2;
        
        var minRadius = 1;
        var maxRadius = Math.max(frameWidth, frameHeight) / 2 * 1.4142;
        var radiusStep = (maxRadius - minRadius) / stepsCount;
        var radius = minRadius;

        
        var opts = {
            cx: centerX,
            cy: centerY,
            radius: radius,
            start_angle: 0,
            end_angle: 359.999
        };
        
        for(var i = 0; i < stepsCount; i++) {
            
            start = PolarToCartesian(opts.cx, opts.cy, opts.radius, opts.end_angle);
            end = PolarToCartesian(opts.cx, opts.cy, opts.radius, opts.start_angle);
            largeArcFlag = opts.end_angle - opts.start_angle <= 180 ? "0" : "1";
            
            var pathData = [
                "M", start.x, start.y,
                "A", opts.radius, opts.radius, 0, largeArcFlag, 0, end.x, end.y,
                "L", opts.cx, opts.cy,
                "Z"
            ].join(" ");
            
            opts.radius += radiusStep;
            
            result.push('<path d="' + pathData + '"/>');
        }
        
        polygonsCache.Circles = result;
    }
    
    return result;
}

function PolarToCartesian(centerX, centerY, radius, angleInDegrees) {
    var angleInRadians = (angleInDegrees - 90) * Math.PI / 180.0;

    return {
        x: centerX + (radius * Math.cos(angleInRadians)),
        y: centerY + (radius * Math.sin(angleInRadians))
    };
}

function DegreesToRadians (angle) {
  return angle * (Math.PI / 180);
}

function ShuffleArray(array) {
    var currentIndex = array.length;
    var temporaryValue, randomIndex;
    
    while (0 !== currentIndex) {
        randomIndex = Math.floor(Math.random() * currentIndex);
        currentIndex -= 1;
        temporaryValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = temporaryValue;
    }
    
    return array;
}

function StackSlides(foreground, background) {
    $(background).css("z-index", 1);
    $(foreground).css("z-index", 2);
}

function GetDuration(slideId) {
    var transitionSpeed = $(slideId).data("transitionSpeed");
    
    var duration = 700;
    if(transitionSpeed == 'Fast') { duration = 400; } 
    else if(transitionSpeed == 'Medium') { duration = 700; } 
    else if(transitionSpeed == 'Slow') { duration = 1500; } 
    
    return duration;
}

function ShowNext() {
      
    if (currentVisiblePage < maxVisiblePage) {
        var prevSlideId = '#slide-' + currentVisiblePage++;
        var slideId = '#slide-' + currentVisiblePage;
        PlayTransition(slideId, prevSlideId);            
    }
}

function ShowPrev() {
      
    if (currentVisiblePage > 1) {
        var prevSlideId = '#slide-' + currentVisiblePage--;
        var slideId = '#slide-' + currentVisiblePage;
        PlayTransition(slideId, prevSlideId);
    }
}