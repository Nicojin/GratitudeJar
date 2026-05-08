window.gratitudeJarJS = {
    initShake: function (dotnetRef) {
        let lastX = 0, lastY = 0, lastZ = 0;
        const threshold = 15;
        let lastShake = 0;

        if (typeof DeviceMotionEvent !== 'undefined') {
            window.addEventListener('devicemotion', function (e) {
                const acc = e.accelerationIncludingGravity;
                if (!acc) return;
                const delta = Math.abs(acc.x - lastX) + Math.abs(acc.y - lastY) + Math.abs(acc.z - lastZ);
                lastX = acc.x; lastY = acc.y; lastZ = acc.z;
                const now = Date.now();
                if (delta > threshold && (now - lastShake) > 1200) {
                    lastShake = now;
                    dotnetRef.invokeMethodAsync('OnDeviceShake');
                }
            });
        }
    }
};
