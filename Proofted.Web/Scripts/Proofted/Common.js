define(["require", "exports"], function(require, exports) {
    (function (Shapes) {
        var Point = (function () {
            function Point(x, y) {
                this.x = x;
                this.y = y;
            }
            Point.prototype.getDist = function () {
                return Math.sqrt(this.x * this.x + this.y * this.y);
            };
            Point.origin = new Point(0, 0);
            return Point;
        })();
        Shapes.Point = Point;        
    })(exports.Shapes || (exports.Shapes = {}));
    var Shapes = exports.Shapes;
})
//@ sourceMappingURL=Common.js.map
