<template>

  <div class="offcanvas offcanvas-fullscreen offcanvas-end" style="width: 1000px; height: 100vh;" data-bs-scroll="true" tabindex="-1"  aria-labelledby="offcanvasWithBothOptionsLabel">
  <div class="offcanvas-header">
    <h5 class="offcanvas-title" id="offcanvasWithBothOptionsLabel"></h5>
    <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
  </div>
  <div>

  <slot></slot>
    
    <svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" xml:space="preserve">
      <defs>
        <filter id="blur0">
          <feGaussianBlur in="SourceGraphic" stdDeviation="0 0" />
        </filter>
        <filter id="blur1">
          <feGaussianBlur in="SourceGraphic" stdDeviation="0 5" />
        </filter>
        <filter id="blur2">
          <feGaussianBlur in="SourceGraphic" stdDeviation="0 10" />
        </filter>
        <filter id="blur3">
          <feGaussianBlur in="SourceGraphic" stdDeviation="0 15" />
        </filter>
        <filter id="blur4">
          <feGaussianBlur in="SourceGraphic" stdDeviation="0 20" />
        </filter>
      </defs>
    </svg>

    </div>
  </div>



</template>