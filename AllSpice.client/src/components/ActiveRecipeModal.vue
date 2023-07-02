<template>

  <!-- NOTE - MODAL CAN BE USED FOR ANY INSTANCE BY USING <slot></slot> TO INSERT CONTENT ---------------------->
  <div class="modal fade" id="activeRecipeModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="activeRecipeModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">

        <slot></slot>

      </div>
    </div>
  </div>

</template>