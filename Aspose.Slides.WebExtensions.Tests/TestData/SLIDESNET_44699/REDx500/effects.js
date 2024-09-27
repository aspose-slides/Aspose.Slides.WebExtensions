
const entranceBouncePath = anime.path('#entranceBouncePath');
const exitBouncePath = anime.path('#exitBouncePath');
const entranceSpiralPath = anime.path('#entranceSpiralPath');
const exitSpiralPath = anime.path('#exitSpiralPath');
const entranceCenterRevolvePath = anime.path('#entranceCenterRevolvePath');
const exitCenterRevolvePath = anime.path('#exitCenterRevolvePath');
const entranceCurveUpDownPath = anime.path('#entranceCurveUpDownPath');
const exitCurveUpDownPath = anime.path('#exitCurveUpDownPath');

function GetAnimationEffect(type, subtype, shapeId, duration, delay, extra) {
    switch (type) {
        case 'EntranceAppear':
            return new EntranceAppearEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitAppear':
            return new ExitAppearEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceFade':
            return new EntranceFadeEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitFade':
            return new ExitFadeEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceFly':
            return new EntranceFlyEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitFly':
            return new ExitFlyEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceAscend':
            return new EntranceAscendDescendEffect(shapeId, duration, delay, 'EntranceAscend', extra);
        case 'EntranceDescend':
            return new EntranceAscendDescendEffect(shapeId, duration, delay, 'EntranceDescend', extra);
        case 'ExitAscend':
            return new ExitAscendDescendEffect(shapeId, duration, delay, 'ExitAscend', extra);
        case 'ExitDescend':
            return new ExitAscendDescendEffect(shapeId, duration, delay, 'ExitDescend', extra);
        case 'EntranceSplit':
            return new EntranceSplitEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitSplit':
            return new ExitSplitEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceWheel':
            return new EntranceWheelEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitWheel':
            return new ExitWheelEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceRandomBars':
            return new EntranceRandomBarsEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitRandomBars':
            return new ExitRandomBarsEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceCircle':
            return new EntranceCircleEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitCircle':
            return new ExitCircleEffect(shapeId, duration, delay, subtype, extra);        
        case 'EntranceBox':
            return new EntranceBoxEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitBox':
            return new ExitBoxEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceDiamond':
            return new EntranceDiamondEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitDiamond':
            return new ExitDiamondEffect(shapeId, duration, delay, subtype, extra);
        case 'EntrancePlus':
            return new EntrancePlusEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitPlus':
            return new ExitPlusEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceBlinds':
            return new EntranceBlindsEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitBlinds':
            return new ExitBlindsEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceWipe':
            return new EntranceWipeEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitWipe':
            return new ExitWipeEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceWedge':
            return new EntranceWedgeEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitWedge':
            return new ExitWedgeEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceDissolve':
            return new EntranceDissolveEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitDissolve':
            return new ExitDissolveEffect(shapeId, duration, delay, subtype, extra);
        case 'EntrancePeek':
            return new EntrancePeekEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitPeek':
            return new ExitPeekEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceStrips':
            return new EntranceStripsEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitStrips':
            return new ExitStripsEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceFadedSwivel':
            return new EntranceFadedSwivelEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitFadedSwivel':
            return new ExitFadedSwivelEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceFadedZoom':
            return new EntranceFadedZoomEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitFadedZoom':
            return new ExitFadedZoomEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceExpand':
            return new EntranceExpandEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitExpand':
            return new ExitExpandEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceZoom':
            return new EntranceZoomEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitZoom':
            return new ExitZoomEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceRiseUp':
            return new EntranceRiseUpEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitRiseUp':
            return new ExitRiseUpEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceCompress':
            return new EntranceCompressEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitCompress':
            return new ExitCompressEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceSpinner':
            return new EntranceSpinnerEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitSpinner':
            return new ExitSpinnerEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceGrowAndTurn':
            return new EntranceGrowAndTurnEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitGrowAndTurn':
            return new ExitGrowAndTurnEffect(shapeId, duration, delay, subtype, extra);
        case 'EntrancePinwheel':
            return new EntrancePinwheelEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitPinwheel':
            return new ExitPinwheelEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceCredits':
            return new EntranceCreditsEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitCredits':
            return new ExitCreditsEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceSwivel':
            return new EntranceSwivelEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitSwivel':
            return new ExitSwivelEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceFloat':
            return new EntranceFloatEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitFloat':
            return new ExitFloatEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceBoomerang':
            return new EntranceBoomerangEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitBoomerang':
            return new ExitBoomerangEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceWhip':
            return new EntranceWhipEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitWhip':
            return new ExitWhipEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceSwish':
            return new EntranceSwishEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitSwish':
            return new ExitSwishEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceBounce':
            return new EntranceBounceEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitBounce':
            return new ExitBounceEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceSpiral':
            return new EntranceSpiralEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitSpiral':
            return new ExitSpiralEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceCenterRevolve':
            return new EntranceCenterRevolveEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitCenterRevolve':
            return new ExitCenterRevolveEffect(shapeId, duration, delay, subtype, extra);
        case 'EntranceCurveUpDown':
            return new EntranceCurveUpDownEffect(shapeId, duration, delay, subtype, extra);
        case 'ExitCurveUpDown':
            return new ExitCurveUpDownEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisSpin':
            return new EmphasisSpinEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisGrowShrink':
            return new EmphasisGrowShrinkEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisFlashBulb':
            return new EmphasisFlashBulbEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisTeeter':
            return new EmphasisTeeterEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisLighten':
            return new EmphasisLightenEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisDarken':
            return new EmphasisDarkenEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisFlicker':
            return new EmphasisFlickerEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisDesaturate':
            return new EmphasisDesaturateEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisTransparency':
            return new EmphasisTransparencyEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisBlink':
            return new EmphasisBlinkEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisChangeFillColor':
            return new EmphasisChangeFillColorEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisBrushOnColor':
            return new EmphasisBrushOnColorEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisColorBlend':
            return new EmphasisColorBlendEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisGrowWithColor':
            return new EmphasisGrowWithColorEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisChangeLineColor':
            return new EmphasisChangeLineColorEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisShimmer':
            return new EmphasisShimmerEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisWave':
            return new EmphasisWaveEffect(shapeId, duration, delay, subtype, extra);
        case 'EmphasisContrastingColor':
            return new EmphasisContrastingColorEffect(shapeId, duration, delay, subtype, extra);    
        case 'EmphasisComplementaryColor':
            return new EmphasisComplementaryColorEffect(shapeId, duration, delay, subtype, extra);    
        case 'EmphasisComplementaryColor2':
            return new EmphasisComplementaryColor2Effect(shapeId, duration, delay, subtype, extra);        
            
        default:
            return new EmptyEffect(shapeId, duration, delay, type);
    }
}

class Effect {
    
    constructor(shapeId, duration, delay, subtype, extra) {
        this.shapeId = shapeId;
        this.duration = duration * 1000;
        this.delay = delay * 1000;
        this.subtype = subtype;
        this.extra = extra;
        
        this.shapeParams = { left: parseInt($(this.shapeId).css('left').replace('px', '')), 
                             top: parseInt($(this.shapeId).css('top').replace('px', '')),
                             width: parseInt($(this.shapeId).css('width').replace('px', '')),
                             height: parseInt($(this.shapeId).css('height').replace('px', '')),
                             opacity: parseInt($(this.shapeId).css('opacity')),
                             background: $(this.shapeId).css('background-color'),
                             border: $(this.shapeId).css('border')
                           };
    }
    
    Prepare() { }
    Play() { this.effectTimeline.play(); }
    Pause() { this.effectTimeline.pause(); }
    Restart() { this.effectTimeline.restart(); }
    Finish() { 
        this.Restore(); 
        $(this.shapeId.replace(' > #', '') + '-clip').empty(); 
    }
    Restore() {
        
        $(this.shapeId).css('left', this.shapeParams.left + 'px');
        $(this.shapeId).css('top', this.shapeParams.top + 'px');
        $(this.shapeId).css('width', this.shapeParams.width + 'px');
        $(this.shapeId).css('height', this.shapeParams.height + 'px');
        $(this.shapeId).css('opacity', this.shapeParams.opacity);
        $(this.shapeId).css('background-color', this.shapeParams.background);
        $(this.shapeId).css('border', this.shapeParams.border);
        
        $(this.shapeId).css({'transform': ''});
        $(this.shapeId).css('clip-path', '');
        $(this.shapeId).show();
    }
}

class EntranceAppearEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        $(this.shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            complete: function(anim) {
                $(shapeId).show();
            }
        });
    
        this.effectTimeline.add({
            duration: this.delay,
            delay: this.delay
        });
    }
}

class ExitAppearEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            complete: function(anim) {
                $(shapeId).hide();
            }
        });
    
        this.effectTimeline.add({
            duration: this.delay,
            delay: this.delay
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}


class EntranceFadeEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 1
        });
    }
}

class ExitFadeEffect extends Effect {
    
    Prepare() {
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 0
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceFlyEffect extends Effect {
    
    Prepare() {
        
        var translate = GetFlyEffectTranslate(this.shapeParams, this.subtype);
        $(this.shapeId).css('transform', 'translateX(' + translate.x + 'px) translateY(' + translate.y + 'px)');
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            translateX: 0,
            translateY: 0
        });
    }
}

class ExitFlyEffect extends Effect {
    
    Prepare() {
        
        var translate = GetFlyEffectTranslate(this.shapeParams, this.subtype);
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            translateX: translate.x,
            translateY: translate.y
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceAscendDescendEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var translate = 60;
        if (this.subtype == 'EntranceAscend')
            translate = -translate;
        
        $(shapeId).css('transform', 'translateY(' + translate + 'px)');
        $(shapeId).css('opacity', '0');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            translateY: 0,
            opacity: 1
        });
    }
}

class ExitAscendDescendEffect extends Effect {
    
    Prepare() {
        
        var translate = 60;
        if (this.subtype == 'ExitAscend')
            translate = -translate;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            translateY: translate,
            opacity: 0
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceSplitEffect extends Effect {
    
    Prepare() {
        
        $(this.shapeId).hide();

        if (this.effectTimeline == null) {                
            var stepsCount = 30;
            var polys = GenerateSplitPolygons(stepsCount, this.shapeParams.width, this.shapeParams.height, this.subtype.includes("Horizontal"), this.subtype.includes("Out"));
            this.effectTimeline = AnimatePolygonsForShape(this.shapeId, polys, this.duration, this.delay);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitSplitEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var stepsCount = 30;
            var polys = GenerateSplitPolygons(stepsCount, this.shapeParams.width, this.shapeParams.height, this.subtype.includes("Horizontal"), !this.subtype.includes("Out"));
            this.effectTimeline = AnimatePolygonsForShapeExit(this.shapeId, polys, this.duration, this.delay, true);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();        
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntranceWheelEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).hide();
        
        if (this.effectTimeline == null) {
            var stepsCount = 180;
            var wheelType = this.subtype == 'Wheel1' ? 'Wheel' : this.subtype;
            var polys = GenerateWheelSectors(wheelType, stepsCount, this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePolygonsForShape(this.shapeId, polys, this.duration, this.delay);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitWheelEffect extends Effect {
    
    Prepare() {
        
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var stepsCount = 180;
            var wheelType = this.subtype == 'Wheel1' ? 'Wheel' : this.subtype;
            var polys = GenerateWheelSectors(wheelType, stepsCount, this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePolygonsForShapeExit(this.shapeId, polys, this.duration, this.delay, false);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntranceRandomBarsEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).hide();
        
        if (this.effectTimeline == null) {
            var bars = GenerateBars(this.subtype == 'Vertical', this.shapeParams.width, this.shapeParams.height, 6, 30, 4);
            ShuffleArray(bars);
            this.effectTimeline = AnimateRandomBarsForShapes(this.shapeId, bars, this.duration, this.delay);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitRandomBarsEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var bars = GenerateBars(this.subtype == 'Vertical', this.shapeParams.width, this.shapeParams.height, 6, 30, 4);
            ShuffleArray(bars);
            this.effectTimeline = AnimateRandomBarsForShapesExit(this.shapeId, bars, this.duration, this.delay);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntranceCircleEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).hide();
        
        if (this.effectTimeline == null) {
            var stepsCount = 150;
            var rings = GenerateRings(stepsCount, this.shapeParams.width, this.shapeParams.height, this.subtype == 'Out');
            this.effectTimeline = AnimatePolygonsForShape(this.shapeId, rings, this.duration, this.delay);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitCircleEffect extends Effect {
    
    Prepare() {
        
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var stepsCount = 150;
            var polys = GenerateRings(stepsCount, this.shapeParams.width, this.shapeParams.height, this.subtype == 'In');
            this.effectTimeline = AnimatePolygonsForShapeExit(this.shapeId, polys, this.duration, this.delay, false);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntranceBoxEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).hide();
        
        if (this.effectTimeline == null) {
            var stepsCount = 150;
            var polys = GenerateShapePolygons(stepsCount, this.subtype == 'Out' ? 'ZoomIn' : 'ZoomOut', this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePolygonsForShape(this.shapeId, polys, this.duration, this.delay);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitBoxEffect extends Effect {
    
    Prepare() {
        
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var stepsCount = 150;
            var polys = GenerateShapePolygons(stepsCount, this.subtype == 'Out' ? 'ZoomIn' : 'ZoomOut', this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePolygonsForShapeExit(this.shapeId, polys, this.duration, this.delay, true);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntranceDiamondEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).hide();
        
        if (this.effectTimeline == null) {
            var stepsCount = 150;
            var polys = GenerateShapePolygons(stepsCount, 'Diamond' + this.subtype, this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePolygonsForShape(this.shapeId, polys, this.duration, this.delay);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitDiamondEffect extends Effect {
    
    Prepare() {
        
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var stepsCount = 150;
            var polys = GenerateShapePolygons(stepsCount, 'Diamond' + this.subtype, this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePolygonsForShapeExit(this.shapeId, polys, this.duration, this.delay, true);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntrancePlusEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).hide();
        
        if (this.effectTimeline == null) {
            var stepsCount = 150;
            var polys = GenerateShapePolygons(stepsCount, 'Plus' + this.subtype, this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePolygonsForShape(this.shapeId, polys, this.duration, this.delay);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitPlusEffect extends Effect {
    
    Prepare() {
        
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var stepsCount = 150;
            var polys = GenerateShapePolygons(stepsCount, 'Plus' + this.subtype, this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePolygonsForShapeExit(this.shapeId, polys, this.duration, this.delay, true);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntranceBlindsEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).hide();
        
        if (this.effectTimeline == null) {
            var stepsCount = 30;
            var polys = GenerateBlindsPolygons(stepsCount, this.shapeParams.width, this.shapeParams.height, this.subtype == 'Horizontal');
            this.effectTimeline = AnimatePolygonsForShape(this.shapeId, polys, this.duration, this.delay);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitBlindsEffect extends Effect {
    
    Prepare() {
        
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var stepsCount = 30;
            var polys = GenerateBlindsPolygons(stepsCount, this.shapeParams.width, this.shapeParams.height, this.subtype == 'Horizontal');
            this.effectTimeline = AnimatePolygonsForShapeExit(this.shapeId, polys, this.duration, this.delay, true);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntranceWipeEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).hide();
        
        if (this.effectTimeline == null) {
            var stepsCount = 40;
            var direction = this.subtype;
            if (direction == 'Bottom')
                direction = 'Up';
            if (direction == 'Top')
                direction = 'Down';
            var polys = GenerateWipePolygons(stepsCount, direction, this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePolygonsForShape(this.shapeId, polys, this.duration, this.delay, false);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitWipeEffect extends Effect {
    
    Prepare() {
        
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var stepsCount = 40;
            var direction = this.subtype;
            if (direction == 'Bottom')
                direction = 'Up';
            if (direction == 'Top')
                direction = 'Down';
            var polys = GenerateWipePolygons(stepsCount, direction, this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePolygonsForShapeExit(this.shapeId, polys, this.duration, this.delay, true);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntranceWedgeEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).hide();
        
        if (this.effectTimeline == null) {
            var stepsCount = 150;
            var sectors = GenerateWheelSectors('Wedge', stepsCount, this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePolygonsForShape(this.shapeId, sectors, this.duration, this.delay);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitWedgeEffect extends Effect {
    
    Prepare() {
        
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var stepsCount = 150;
            var sectors = GenerateWheelSectors('Wedge', stepsCount, this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePolygonsForShapeExit(this.shapeId, sectors, this.duration, this.delay, false);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntranceDissolveEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).hide();
        
        if (this.effectTimeline == null) {
            var tiles = GenerateDissolvePolygons(this.shapeParams.width, this.shapeParams.height, 32, 24);
            ShuffleArray(tiles);
            this.effectTimeline = AnimateDissolveForShapes(30, this.shapeId, tiles, this.duration, this.delay);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitDissolveEffect extends Effect {
    
    Prepare() {
        
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var tiles = GenerateDissolvePolygons(this.shapeParams.width, this.shapeParams.height, 32, 24);
            ShuffleArray(tiles);
            this.effectTimeline = AnimateDissolveForShapesExit(30, this.shapeId, tiles, this.duration, this.delay, false);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntrancePeekEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).hide();
        
        if (this.effectTimeline == null) {
            var stepsCount = 80;
            var direction = this.subtype;
            if (direction == 'Bottom')
                direction = 'Down';
            if (direction == 'Top')
                direction = 'Up';
            var polys = GenerateWipePolygons(stepsCount, direction, this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePeekForShape(this.shapeId, polys, this.duration, this.delay, this.subtype);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitPeekEffect extends Effect {
    
    Prepare() {
        
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var stepsCount = 80;
            var direction = this.subtype;
            if (direction == 'Bottom')
                direction = 'Up';
            if (direction == 'Top')
                direction = 'Down';
            if (direction == 'Left')
                direction = 'Right';
            else if (direction == 'Right')
                direction = 'Left';
            var polys = GenerateWipePolygons(stepsCount, direction, this.shapeParams.width, this.shapeParams.height);
            this.effectTimeline = AnimatePeekForShapeExit(this.shapeId, polys, this.duration, this.delay, this.subtype, false);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntranceStripsEffect extends Effect {
    
    Prepare() {
                
        $(this.shapeId).hide();
        
        if (this.effectTimeline == null) {
            var polys = GenerateStripsPolygons(this.shapeParams.width, this.shapeParams.height, 15, this.subtype);
            this.effectTimeline = AnimatePolygonsForShape(this.shapeId, polys, this.duration, this.delay, true);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class ExitStripsEffect extends Effect {
    
    Prepare() {
        
        $(this.shapeId).show();
        
        if (this.effectTimeline == null) {
            var polys = GenerateStripsPolygons(this.shapeParams.width, this.shapeParams.height, 15, this.subtype);
            this.effectTimeline = AnimatePolygonsForShapeExit(this.shapeId, polys, this.duration, this.delay, true, true);
        }
        
        clipContentCache[this.effectTimeline.id] = "";
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
    
    Finish() {
        $(this.shapeId).hide();
        $(this.shapeId.replace(' > #', '') + '-clip').empty();
    }
}

class EntranceFadedSwivelEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0');
        $(shapeId).css('transform', 'rotateY(' + 180 + 'deg)');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 1,
            rotateY: [0, 180, 0]
        });
    }
}

class ExitFadedSwivelEffect extends Effect {
    
    Prepare() {
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 0,
            rotateY: [180, 0, 180]
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceExpandEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0');
        $(shapeId).css('transform', 'scaleX(' + 0.7 + ')');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 1,
            scaleX: 1
        });
    }
}

class ExitExpandEffect extends Effect {
    
    Prepare() {
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 0,
            scaleX: 0.7
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceFadedZoomEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0');
        $(shapeId).css('transform', 'scale(' + 0.1 + ')');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 1,
            scale: 1
        });
    }
}

class ExitFadedZoomEffect extends Effect {
    
    Prepare() {
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 0,
            scale: 0.1
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceZoomEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('transform', 'scale(' + 0.05 + ')');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            scale: 1
        });
    }
}

class ExitZoomEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear',
            complete: function(anim) {
                $(shapeId).hide();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            scale: 0.05
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceRiseUpEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        var riseDistance = frameHeight - this.shapeParams.top + 100;
        $(shapeId).css('opacity', '0');
        $(shapeId).css('transform', 'translateY(' + riseDistance + 'px)');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'easeInOutBack',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 1,
            translateY: 0
        });
    }
}

class ExitRiseUpEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        var riseDistance = frameHeight - this.shapeParams.top + 100;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'easeInOutBack',
            complete: function(anim) {
                $(shapeId).hide();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 0,
            translateY: riseDistance
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}


class EntranceCompressEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0');
        $(shapeId).css('transform', 'scaleX(' + 2 + ')');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'easeInCubic',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 1,
            scaleX: 1
        });
    }
}

class ExitCompressEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear',
            complete: function(anim) {
                $(shapeId).hide();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 0,
            scaleX: 2
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceSpinnerEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0');
        $(shapeId).css('transform', 'scale(' + 0.2 + ') rotateZ(' + 180 + 'deg)');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'easeOutCubic',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 1,
            scale: 1,
            rotateZ: -360
        });
    }
}

class ExitSpinnerEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        var riseDistance = frameHeight - this.shapeParams.top + 100;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'easeInCubic',
            complete: function(anim) {
                $(shapeId).hide();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 0,
            scale: 0.2,
            rotateZ: 360
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceGrowAndTurnEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0');
        $(shapeId).css('transform', 'scale(' + 0.2 + ') rotateZ(' + 90 + 'deg)');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 1,
            scale: 1,
            rotateZ: 0
        });
    }
}

class ExitGrowAndTurnEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        var riseDistance = frameHeight - this.shapeParams.top + 100;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear',
            complete: function(anim) {
                $(shapeId).hide();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 0,
            scale: 0.2,
            rotateZ: 90
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntrancePinwheelEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0');
        $(shapeId).css('transform', 'scale(' + 0.1 + ') rotateZ(' + 0 + 'deg)');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 1,
            scale: 1,
            rotateZ: -720
        });
    }
}

class ExitPinwheelEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        var riseDistance = frameHeight - this.shapeParams.top + 100;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'easeInCubic',
            complete: function(anim) {
                $(shapeId).hide();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 0,
            scale: 0.1,
            rotateZ: 720
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceCreditsEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        var lowPosition = frameHeight - this.shapeParams.top + this.shapeParams.height;
        var highPosition = -this.shapeParams.top - 2.5 * this.shapeParams.height;
        $(shapeId).css('transform', 'translateY(' + lowPosition + 'px)');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            translateY: highPosition
        });
    }
    
    Finish() {
        $(this.shapeId).hide();
    }
}

class ExitCreditsEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        var lowPosition = frameHeight - this.shapeParams.top + 2.5 * this.shapeParams.height;
        var highPosition = -this.shapeParams.top - 2 * this.shapeParams.height;
        $(shapeId).css('transform', 'translateY(' + highPosition + 'px)');
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear',
            complete: function(anim) {
                $(shapeId).hide();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            translateY: lowPosition
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceSwivelEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('transform', 'rotateY(' + -90 + 'deg)');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            rotateY: 360
        });
    }
}

class ExitSwivelEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear',
            complete: function(anim) {
                $(shapeId).hide();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            rotateY: -450
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceFloatEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0');
        $(shapeId).css('transform', 'translateX(' + 400 + 'px) translateY(' + -300 + 'px) rotateZ(' + -90 + 'deg)');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'easeOutBack',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 1,
            translateX: 0,
            translateY: 0,
            rotateZ: 0
        });
    }
}

class ExitFloatEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'easeInBack',
            complete: function(anim) {
                $(shapeId).hide();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: 0,
            translateX: 400,
            translateY: -300,
            rotateZ: -90
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceBoomerangEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0');
        $(shapeId).css('transform', 'translateX(' + 300 + 'px) translateY(' + -150 + 'px) rotateZ(' + -90 + 'deg) scaleX(' + 1.2 + ')');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
        
        this.effectTimeline.add({
            duration: this.duration / 2,
            delay: this.delay,
            opacity: 0.7,
            translateX: [300, 0],
            translateY: [-150, 50],
            rotateZ: [-90, 0],
            scaleX: [1.2, 0.1]
        });
        
        this.effectTimeline.add({
            duration: this.duration / 2,
            delay: this.delay,
            opacity: 1,
            translateX: 0,
            translateY: 0,
            rotateZ: 0,
            scaleX: 1
        });
    }
}

class ExitBoomerangEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear',
            complete: function(anim) {
                $(shapeId).hide();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration / 2,
            delay: this.delay,
            opacity: 0.7,
            translateX: 0,
            translateY: 50,
            rotateZ: 0,
            scaleX: 0.1
        });
        
        this.effectTimeline.add({
            duration: this.duration / 2,
            delay: this.delay,
            opacity: 0,
            translateX: 300,
            translateY: -150,
            rotateZ: -90,
            scaleX: 1.2
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceWhipEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0');
        $(shapeId).css('transform', 'translateX(' + 20 + 'px) scale(' + 0.2 + ')');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
        
        this.effectTimeline.add({
            duration: this.duration / 2,
            delay: this.delay,
            opacity: 0.7,
            translateX: [20, 80],
            scale: [0.2, 1.2]
        });
        
        this.effectTimeline.add({
            duration: this.duration / 2,
            delay: this.delay,
            opacity: 1,
            translateX: 0,
            scale: 1
        });
    }
}

class ExitWhipEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear',
            complete: function(anim) {
                $(shapeId).hide();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration / 2,
            delay: this.delay,
            opacity: 0.7,
            translateX: 80,
            scale: 1.2
        });
        
        this.effectTimeline.add({
            duration: this.duration / 2,
            delay: this.delay,
            opacity: 0,
            translateX: 20,
            scale: 0.2
        });
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceSwishEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('transform', 'translateY(' + (-this.shapeParams.top - 1.5 * this.shapeParams.height) + 'px) rotateZ(' + -30 + 'deg)');
        $(shapeId).hide();
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            begin: function(anim) {
                $(shapeId).show();
            },
        });
        
        this.effectTimeline.add({
            duration: this.duration / 2 * 3,
            delay: this.delay,
            translateX: 0,
            translateY: -20,
        });
        
        this.effectTimeline.add({
            duration: this.duration / 3,
            delay: this.delay,
            rotateZ: 15,
            translateX: 40,
            translateY: -40,
        });
        
        this.effectTimeline.add({
            duration: this.duration / 3,
            delay: this.delay,
            rotateZ: 0,
            translateX: 0,
            translateY: 0,
        });
    }
}

class ExitSwishEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear',
            complete: function(anim) {
                $(shapeId).hide();
            },
        });
    
        this.effectTimeline.add({
            duration: this.duration / 3,
            delay: this.delay,
            rotateZ: 30,
            translateX: 50,
            translateY: 75
        });
        
        this.effectTimeline.add({
            duration: this.duration / 3 * 2,
            delay: this.delay,
            rotateZ: 25,
            translateX: 0.5 * (frameHeight - this.shapeParams.top + 0.5 * this.shapeParams.height),
            translateY: frameHeight - this.shapeParams.top + 0.5 * this.shapeParams.height
        });
        
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceBounceEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0.0');
        
        if (this.effectTimeline == null) {
            var entranceBouncePath = anime.path('#entranceBouncePath');
            
            this.effectTimeline = anime.timeline({
                targets: shapeId,
                easing: 'easeInOutQuart',
                autoplay: false,
                duration: this.duration,
                delay: this.delay,
            });
            
            this.effectTimeline.add({
                duration: this.duration,
                opacity: 1,
                translateX: entranceBouncePath('x'),
                translateY: entranceBouncePath('y')
            });
        }
    }
}

class ExitBounceEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        if (this.effectTimeline == null) {            
            var exitBouncePath = anime.path('#exitBouncePath');
                
            this.effectTimeline = anime.timeline({
                targets: shapeId,
                easing: 'easeInQuint',
                autoplay: false,
                duration: this.duration,
                delay: this.delay
            });
            
            this.effectTimeline.add({
                duration: this.duration,
                opacity: 0,
                translateX: exitBouncePath('x'),
                translateY: exitBouncePath('y')
            });
        }
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceSpiralEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0.0');
        $(shapeId).css('transform', 'scale(' + 0.1 + ')');
        
        if (this.effectTimeline == null) {
            var entranceSpiralPath = anime.path('#entranceSpiralPath');
            
            this.effectTimeline = anime.timeline({
                targets: shapeId,
                easing: 'linear',
                autoplay: false,
                duration: this.duration,
                delay: this.delay,
            });
            
            this.effectTimeline.add({
                duration: this.duration,
                opacity: 1,
                scale: 1,
                translateX: entranceSpiralPath('x'),
                translateY: entranceSpiralPath('y')
            });
        }
    }
}

class ExitSpiralEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        if (this.effectTimeline == null) {            
            var exitSpiralPath = anime.path('#exitSpiralPath');
                
            this.effectTimeline = anime.timeline({
                targets: shapeId,
                easing: 'linear',
                autoplay: false,
                duration: this.duration,
                delay: this.delay
            });
            
            this.effectTimeline.add({
                duration: this.duration,
                opacity: 0,
                scale: 0.9,
                translateX: exitSpiralPath('x'),
                translateY: exitSpiralPath('y')
            });
        }
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceCenterRevolveEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0.0');
        
        if (this.effectTimeline == null) {
            var entranceCenterRevolvePath = anime.path('#entranceCenterRevolvePath');
            
            this.effectTimeline = anime.timeline({
                targets: shapeId,
                easing: 'linear',
                autoplay: false,
                duration: this.duration,
                delay: this.delay,
            });
            
            this.effectTimeline.add({
                duration: this.duration / 10,
                opacity: 1,
            });
            
            this.effectTimeline.add({
                duration: this.duration,
                translateX: entranceCenterRevolvePath('x'),
                translateY: entranceCenterRevolvePath('y')
            });
        }
    }
}

class ExitCenterRevolveEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        if (this.effectTimeline == null) {            
            var exitCenterRevolvePath = anime.path('#exitCenterRevolvePath');
                
            this.effectTimeline = anime.timeline({
                targets: shapeId,
                easing: 'linear',
                autoplay: false,
                duration: this.duration,
                delay: this.delay
            });
            
            this.effectTimeline.add({
                duration: this.duration,
                translateX: exitCenterRevolvePath('x'),
                translateY: exitCenterRevolvePath('y')
            });
            
            this.effectTimeline.add({
                duration: this.duration / 10,
                opacity: 0,
            });
        }
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EntranceCurveUpDownEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        $(shapeId).css('opacity', '0.0');
        $(shapeId).css('transform', 'scale(' + 1.7 + ')');
        
        if (this.effectTimeline == null) {
            var entranceCurveUpDownPath = anime.path('#entranceCurveUpDownPath');
            
            this.effectTimeline = anime.timeline({
                targets: shapeId,
                easing: 'linear',
                autoplay: false,
                duration: this.duration,
                delay: this.delay,
            });
            
            this.effectTimeline.add({
                duration: this.duration,
                opacity: 1,
                scale: 1,
                translateX: entranceCurveUpDownPath('x'),
                translateY: entranceCurveUpDownPath('y')
            });
        }
    }
}

class ExitCurveUpDownEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        if (this.effectTimeline == null) {            
            var exitCurveUpDownPath = anime.path('#exitCurveUpDownPath');
                
            this.effectTimeline = anime.timeline({
                targets: shapeId,
                easing: 'linear',
                autoplay: false,
                duration: this.duration,
                delay: this.delay
            });
            
            this.effectTimeline.add({
                duration: this.duration,
                opacity: 0,
                scale: 1.7,
                translateX: exitCurveUpDownPath('x'),
                translateY: exitCurveUpDownPath('y')
            });
        }
    }
    
    Finish() {
        $(this.shapeId).hide();        
    }
}

class EmphasisSpinEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            rotateZ: this.extra    
        });
    }
    
    Finish() {
        $(this.shapeId).css('transform', 'rotateZ(' + this.extra + 'deg)');
    }
}

class EmphasisGrowShrinkEffect extends Effect {
    
    Prepare() {
        var shapeId = this.shapeId;
        var coefs = this.extra.match(/^{X=(-?[\d.]+), Y=(-?[\d.]+)}$/i);
        this.xcoef = parseFloat(coefs[1]) / 100;
        this.ycoef = parseFloat(coefs[2]) / 100;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            scaleX: this.xcoef,
            scaleY: this.ycoef
        });
    }
    
    Finish() {
        $(this.shapeId).css('transform', 'scaleX(' + this.xcoef + ') scaleY(' + this.ycoef + ')');
    }
}

class EmphasisFlashBulbEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var emphasisColor = ShadeBlendConvert(0.4, $(shapeId).css('background-color'));
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            direction: 'alternate',
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            scale: 1.1,
            background: emphasisColor
        });
    }
}

class EmphasisLightenEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var originalColor = $(shapeId).css('background-color');
        var emphasisColor = ShadeBlendConvert(0.5, $(shapeId).css('background-color'), false, true);
        this.emphasisColor = emphasisColor;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            background: [originalColor, emphasisColor]
        });
    }
    
    Finish() { 
        $(this.shapeId).css('backgroundColor', this.emphasisColor);    
    }
}

class EmphasisDarkenEffect extends Effect {

    Prepare() {
        
        var shapeId = this.shapeId;
        var originalColor = $(shapeId).css('background-color');
        var emphasisColor = ShadeBlendConvert(-0.5, $(shapeId).css('background-color'), false, true);
        this.emphasisColor = emphasisColor;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
        
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            background: [originalColor, emphasisColor]
        });
    }
    
    Finish() { 
        $(this.shapeId).css('backgroundColor', this.emphasisColor);    
    }
}

class EmphasisFlickerEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var emphasisColor = 'rgb(255,255,255)';
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            direction: 'alternate',
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            background: emphasisColor
        });
    }
}

class EmphasisDesaturateEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var originalColor = $(shapeId).css('background-color');
        var rgbstring = originalColor.match(/^rgba?\s*\(\s*(\d+)\s*,\s*(\d+)\s*,\s*(\d+)\s*(,\s*([\.\d]+)\s*)?\)$/i);
        var alpha = rgbstring[5] ?? '1';
        var rgb = [parseInt(rgbstring[1]), parseInt(rgbstring[2]), parseInt(rgbstring[3])];
        var grayColor = rgb[0] * 0.3086 + rgb[1] * 0.6094 + rgb[2] * 0.0820;
        var sat = 0.2;
        var emphasisColor = 'rgb(' + Math.round(rgb[0] * sat + grayColor * (1-sat)) + ',' + Math.round(rgb[1] * sat + grayColor * (1-sat)) + ',' + Math.round(rgb[2] * sat + grayColor * (1-sat)) + ',' + alpha + ')';
        this.emphasisColor = emphasisColor;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            background: [originalColor, emphasisColor]
        });
    }
    
    Finish() { 
        $(this.shapeId).css('backgroundColor', this.emphasisColor);    
    }
}

class EmphasisTeeterEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
        
        this.effectTimeline.add({
            duration: this.duration / 8,
            delay: this.delay,
            rotateZ: 5
        });
        
        this.effectTimeline.add({
            duration: this.duration / 4,
            delay: this.delay,
            rotateZ: -5
        });
        
        this.effectTimeline.add({
            duration: this.duration / 4,
            delay: this.delay,
            rotateZ: 5
        });
        
        this.effectTimeline.add({
            duration: this.duration / 4,
            delay: this.delay,
            rotateZ: -5
        });
        
        this.effectTimeline.add({
            duration: this.duration / 8,
            delay: this.delay,
            rotateZ: 0
        });
    }
}

class EmphasisTransparencyEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
        
        this.effectTimeline.add({
            duration: 10,
            opacity: this.extra
        });
        
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            opacity: this.extra
        });
    }
    
    Finish() {
        $(this.shapeId).css('opacity', this.extra);
    }
}

class EmphasisBlinkEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
        
        this.effectTimeline.add({
            duration: this.duration / 2,
            delay: this.delay,
            complete: function(anim) {
                $(shapeId).hide();
            }
        });
        
        this.effectTimeline.add({
            duration: this.duration / 2,
            delay: this.delay,
            complete: function(anim) {
                $(shapeId).show();
            }
        });
    }
}

class EmphasisChangeFillColorEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var originalColor = $(shapeId).css('background-color');
        var emphasisColor = this.extra;
        this.emphasisColor = emphasisColor;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
        
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            background: [originalColor, emphasisColor]
        });
    }
    
    Finish() { 
        $(this.shapeId).css('backgroundColor', this.emphasisColor);    
    }
}

class EmphasisBrushOnColorEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var originalColor = $(shapeId).css('background-color');
        var emphasisColor = this.extra;
        this.emphasisColor = emphasisColor;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
        
        this.effectTimeline.add({
            duration: 10,
            background: [originalColor, emphasisColor]
        });
        
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            background: emphasisColor
        });
    }
    
    Finish() { 
        $(this.shapeId).css('backgroundColor', this.emphasisColor);    
    }
}

class EmphasisColorBlendEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var originalColor = $(shapeId).css('background-color');
        var emphasisColor = this.extra;
        this.emphasisColor = emphasisColor;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
        
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            background: [originalColor, emphasisColor]
        });
    }
    
    Finish() { 
        $(this.shapeId).css('backgroundColor', this.emphasisColor);    
    }
}

class EmphasisGrowWithColorEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var originalColor = $(shapeId).css('background-color');
        var emphasisColor = this.extra;
        this.emphasisColor = emphasisColor;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
        
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            background: [originalColor, emphasisColor]
        });
    }
    
    Finish() { 
        $(this.shapeId).css('backgroundColor', this.emphasisColor);    
    }
}

class EmphasisChangeLineColorEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var originalColor = $(shapeId).css('border-style') != 'none' ? $(shapeId).css('border-color') : $(shapeId).css('background-color');
        var emphasisColor = this.extra;
        this.emphasisColor = emphasisColor;
        
        if ($(shapeId).css('border-style') == 'none') {
            $(this.shapeId).css('border-style', 'solid');    
            $(this.shapeId).css('border-width', '1px');
            $(this.shapeId).css('border-color', originalColor);
        }
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
        
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            borderColor: [originalColor, emphasisColor]
        });
    }
    
    Finish() { 
        $(this.shapeId).css('border-color', this.emphasisColor);    
    }
}

class EmphasisShimmerEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear',
            direction: 'alternate'
        });
        
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            rotateY: '-10deg',
            rotateZ: '-5deg',
            scaleX: 0.8,
            skew: '5deg',
            translateX: '10px',
            translateY: '-20px',
            perspective: '500px'
        });
    }
}

class EmphasisWaveEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        
        var coefs = this.extra.match(/^{X=(-?[\d.]+), Y=(-?[\d.]+)}$/i);
        var xcoef = parseFloat(coefs[1]);
        var    ycoef = parseFloat(coefs[2]);        
        
        var originalLeft = parseFloat($(shapeId).css('left'));
        var originalTop = parseFloat($(shapeId).css('top'));
        
        var emphasisLeft = originalLeft + 15 * xcoef;
        var emphasisTop = originalTop + 15 * ycoef;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
        
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            rotateZ: [0, 20, -20, 0],
            left: [originalLeft, emphasisLeft, originalLeft],
            top: [originalTop, emphasisTop, originalTop]
        });
    }
}

class EmphasisContrastingColorEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var originalColor = $(shapeId).css('background-color');
        var rgbstring = originalColor.match(/^rgba?\s*\(\s*(\d+)\s*,\s*(\d+)\s*,\s*(\d+)\s*(,\s*([\.\d]+)\s*)?\)$/i);
        var alpha = rgbstring[5] ?? '1';        
        var rgb = [parseInt(rgbstring[1]), parseInt(rgbstring[2]), parseInt(rgbstring[3])];
        
        var originalHSL = rgbToHsl(rgb[0], rgb[1], rgb[2]);
        var newHue1 = ((originalHSL[0] * 360 + 45) % 360) / 360;
        var newHue2 = ((originalHSL[0] * 360 + 90) % 360) / 360;
        var newHue3 = ((originalHSL[0] * 360 + 135) % 360) / 360;
        var newHue4 = ((originalHSL[0] * 360 + 180) % 360) / 360;
        var emphasisRGB1 = hslToRgb(newHue1, originalHSL[1], originalHSL[2]);
        var emphasisRGB2 = hslToRgb(newHue2, originalHSL[1], originalHSL[2]);
        var emphasisRGB3 = hslToRgb(newHue3, originalHSL[1], originalHSL[2]);
        var emphasisRGB4 = hslToRgb(newHue4, originalHSL[1], originalHSL[2]);
        var emphasisColor1 = 'rgba(' + emphasisRGB1[0] + ',' + emphasisRGB1[1] + ',' + emphasisRGB1[2] + ',' + alpha + ')';
        var emphasisColor2 = 'rgba(' + emphasisRGB2[0] + ',' + emphasisRGB2[1] + ',' + emphasisRGB2[2] + ',' + alpha + ')';
        var emphasisColor3 = 'rgba(' + emphasisRGB3[0] + ',' + emphasisRGB3[1] + ',' + emphasisRGB3[2] + ',' + alpha + ')';
        var emphasisColor4 = 'rgba(' + emphasisRGB4[0] + ',' + emphasisRGB4[1] + ',' + emphasisRGB4[2] + ',' + alpha + ')';
        this.emphasisColor = emphasisColor4;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            background: [originalColor, emphasisColor1, emphasisColor2, emphasisColor3, emphasisColor4]
        });
    }
    
    Finish() { 
        $(this.shapeId).css('backgroundColor', this.emphasisColor);    
    }
}

class EmphasisComplementaryColorEffect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var originalColor = $(shapeId).css('background-color');
        var rgbstring = originalColor.match(/^rgba?\s*\(\s*(\d+)\s*,\s*(\d+)\s*,\s*(\d+)\s*(,\s*([\.\d]+)\s*)?\)$/i);
        var alpha = rgbstring[5] ?? '1';    
        var rgb = [parseInt(rgbstring[1]), parseInt(rgbstring[2]), parseInt(rgbstring[3])];
        
        var originalHSL = rgbToHsl(rgb[0], rgb[1], rgb[2]);
        var newHue1 = ((originalHSL[0] * 360 + 120) % 360) / 360;
        var newHue2 = ((originalHSL[0] * 360 + 240) % 360) / 360;
        var newHue3 = ((originalHSL[0] * 360 + 360) % 360) / 360;
        var newHue4 = ((originalHSL[0] * 360 + 480) % 360) / 360;
        var emphasisRGB1 = hslToRgb(newHue1, originalHSL[1], originalHSL[2]);
        var emphasisRGB2 = hslToRgb(newHue2, originalHSL[1], originalHSL[2]);
        var emphasisRGB3 = hslToRgb(newHue3, originalHSL[1], originalHSL[2]);
        var emphasisRGB4 = hslToRgb(newHue4, originalHSL[1], originalHSL[2]);
        var emphasisColor1 = 'rgba(' + emphasisRGB1[0] + ',' + emphasisRGB1[1] + ',' + emphasisRGB1[2] + ',' + alpha + ')';
        var emphasisColor2 = 'rgba(' + emphasisRGB2[0] + ',' + emphasisRGB2[1] + ',' + emphasisRGB2[2] + ',' + alpha + ')';
        var emphasisColor3 = 'rgba(' + emphasisRGB3[0] + ',' + emphasisRGB3[1] + ',' + emphasisRGB3[2] + ',' + alpha + ')';
        var emphasisColor4 = 'rgba(' + emphasisRGB4[0] + ',' + emphasisRGB4[1] + ',' + emphasisRGB4[2] + ',' + alpha + ')';
        this.emphasisColor = emphasisColor4;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            background: [originalColor, emphasisColor1, emphasisColor2, emphasisColor3, emphasisColor4]
        });
    }
    
    Finish() { 
        $(this.shapeId).css('backgroundColor', this.emphasisColor);    
    }
}

class EmphasisComplementaryColor2Effect extends Effect {
    
    Prepare() {
        
        var shapeId = this.shapeId;
        var originalColor = $(shapeId).css('background-color');
        
        var rgbstring = originalColor.match(/^rgba?\s*\(\s*(\d+)\s*,\s*(\d+)\s*,\s*(\d+)\s*(,\s*([\.\d]+)\s*)?\)$/i);
        var alpha = rgbstring[5] ?? '1';
        
        var rgb = [parseInt(rgbstring[1]), parseInt(rgbstring[2]), parseInt(rgbstring[3])];
        
        var originalHSL = rgbToHsl(rgb[0], rgb[1], rgb[2]);
        var newHue1 = ((originalHSL[0] * 360 + 60) % 360) / 360;
        var newHue2 = ((originalHSL[0] * 360 + 120) % 360) / 360;
        var newHue3 = ((originalHSL[0] * 360 + 180) % 360) / 360;
        var newHue4 = ((originalHSL[0] * 360 + 240) % 360) / 360;
        var emphasisRGB1 = hslToRgb(newHue1, originalHSL[1], originalHSL[2]);
        var emphasisRGB2 = hslToRgb(newHue2, originalHSL[1], originalHSL[2]);
        var emphasisRGB3 = hslToRgb(newHue3, originalHSL[1], originalHSL[2]);
        var emphasisRGB4 = hslToRgb(newHue4, originalHSL[1], originalHSL[2]);
        var emphasisColor1 = 'rgba(' + emphasisRGB1[0] + ',' + emphasisRGB1[1] + ',' + emphasisRGB1[2] + ',' + alpha + ')';
        var emphasisColor2 = 'rgba(' + emphasisRGB2[0] + ',' + emphasisRGB2[1] + ',' + emphasisRGB2[2] + ',' + alpha + ')';
        var emphasisColor3 = 'rgba(' + emphasisRGB3[0] + ',' + emphasisRGB3[1] + ',' + emphasisRGB3[2] + ',' + alpha + ')';
        var emphasisColor4 = 'rgba(' + emphasisRGB4[0] + ',' + emphasisRGB4[1] + ',' + emphasisRGB4[2] + ',' + alpha + ')';
        this.emphasisColor = emphasisColor4;
        
        this.effectTimeline = anime.timeline({
            targets: shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay,
            background: [originalColor, emphasisColor1, emphasisColor2, emphasisColor3, emphasisColor4]
        });
    }
    
    Finish() { 
        $(this.shapeId).css('backgroundColor', this.emphasisColor);    
    }
}

class EmptyEffect extends Effect {
    
    Prepare() {
        
        console.log(this.subtype + ' is not implemented.');
        
        this.effectTimeline = anime.timeline({
            targets: this.shapeId,
            autoplay: false,
            easing: 'linear'
        });
    
        this.effectTimeline.add({
            duration: this.duration,
            delay: this.delay
        });
    }
}

function GetFlyEffectTranslate(shapeParams, subtype) {
    
    switch (subtype) {
        case 'Left':
            return {x: - shapeParams.left - shapeParams.width - 5, y: 0};
        case 'Top':
            return {x: 0, y: - shapeParams.top - shapeParams.height - 5};
        case 'Right':
            return {x: frameWidth - shapeParams.left + 5, y: 0};
        case 'Bottom':
            return {x: 0, y: frameHeight - shapeParams.top + 5};
        case 'TopLeft':
            return {x: - shapeParams.left - shapeParams.width - 5, y: - shapeParams.top - shapeParams.height - 5};
        case 'TopRight':
            return {x: frameWidth - shapeParams.left + 5, y: - shapeParams.top - shapeParams.height - 5};
        case 'BottomLeft':
            return {x: - shapeParams.left - shapeParams.width - 5, y: frameHeight - shapeParams.top + 5};
        case 'BottomRight':
            return {x: frameWidth - shapeParams.left + 5, y: frameHeight - shapeParams.top + 5};
    }
}

function ShadeBlendConvert (p,c0,c1,l){
    let r,g,b,P,f,t,h,i=parseInt,m=Math.round,a=typeof(c1)=="string";
    if(typeof(p)!="number"||p<-1||p>1||typeof(c0)!="string"||(c0[0]!='r'&&c0[0]!='#')||(c1&&!a))return null;
    if(!this.pSBCr)this.pSBCr=(d)=>{
        let n=d.length,x={};
        if(n>9){
            [r,g,b,a]=d=d.split(","),n=d.length;
            if(n<3||n>4)return null;
            x.r=i(r[3]=="a"?r.slice(5):r.slice(4)),x.g=i(g),x.b=i(b),x.a=a?parseFloat(a):-1
        }else{
            if(n==8||n==6||n<4)return null;
            if(n<6)d="#"+d[1]+d[1]+d[2]+d[2]+d[3]+d[3]+(n>4?d[4]+d[4]:"");
            d=i(d.slice(1),16);
            if(n==9||n==5)x.r=d>>24&255,x.g=d>>16&255,x.b=d>>8&255,x.a=m((d&255)/0.255)/1000;
            else x.r=d>>16,x.g=d>>8&255,x.b=d&255,x.a=-1
        }return x};
    h=c0.length>9,h=a?c1.length>9?true:c1=="c"?!h:false:h,f=this.pSBCr(c0),P=p<0,t=c1&&c1!="c"?this.pSBCr(c1):P?{r:0,g:0,b:0,a:-1}:{r:255,g:255,b:255,a:-1},p=P?p*-1:p,P=1-p;
    if(!f||!t)return null;
    if(l)r=m(P*f.r+p*t.r),g=m(P*f.g+p*t.g),b=m(P*f.b+p*t.b);
    else r=m((P*f.r**2+p*t.r**2)**0.5),g=m((P*f.g**2+p*t.g**2)**0.5),b=m((P*f.b**2+p*t.b**2)**0.5);
    a=f.a,t=t.a,f=a>=0||t>=0,a=f?a<0?t:t<0?a:a*P+t*p:0;
    if(h)return"rgb"+(f?"a(":"(")+r+","+g+","+b+(f?","+m(a*1000)/1000:"")+")";
    else return"#"+(4294967296+r*16777216+g*65536+b*256+(f?m(a*255):0)).toString(16).slice(1,f?undefined:-2)
}

function rgbToHsl(r, g, b){
    r /= 255, g /= 255, b /= 255;
    var max = Math.max(r, g, b), min = Math.min(r, g, b);
    var h, s, l = (max + min) / 2;

    if(max == min){
        h = s = 0; // achromatic
    }else{
        var d = max - min;
        s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
        switch(max){
            case r: h = (g - b) / d + (g < b ? 6 : 0); break;
            case g: h = (b - r) / d + 2; break;
            case b: h = (r - g) / d + 4; break;
        }
        h /= 6;
    }

    return [h, s, l];
}

function hslToRgb(h, s, l){
    var r, g, b;

    if(s == 0){
        r = g = b = l; // achromatic
    }else{
        var hue2rgb = function hue2rgb(p, q, t){
            if(t < 0) t += 1;
            if(t > 1) t -= 1;
            if(t < 1/6) return p + (q - p) * 6 * t;
            if(t < 1/2) return q;
            if(t < 2/3) return p + (q - p) * (2/3 - t) * 6;
            return p;
        }

        var q = l < 0.5 ? l * (1 + s) : l + s - l * s;
        var p = 2 * l - q;
        r = hue2rgb(p, q, h + 1/3);
        g = hue2rgb(p, q, h);
        b = hue2rgb(p, q, h - 1/3);
    }

    return [Math.round(r * 255), Math.round(g * 255), Math.round(b * 255)];
}
