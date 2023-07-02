<template>

  <!-- NOTE - MODAL CAN BE USED FOR ANY INSTANCE BY USING <slot></slot> TO INSERT CONTENT ---------------------->
  <div class="modal fade" id="activeRecipeModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="activeRecipeModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">

        <slot>
          <!-- FIXME I'M DOING SOMETHING WRONG WHEN TRYING TO SLOT THIS MODAL INTO THE RECIPE AND ACTIVE RECIPE COMPONENTS. WILL FIX LATER TONIGHT OR TOMM AFTER MY BRAIN HAS HAD A BREAK HAHAHA! -->
        </slot>

      </div>
    </div>
  </div>

</template>