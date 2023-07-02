<template>

  <div class="modal-body row" v-if="recipe">
    <button type="button" class="btn-close" data-bs-dismiss="modal" data-bs-target="#recipeDetailsModal"
      aria-label="Close"></button>



  </div>

</template>

<script>

export default {
  props: {
    recipe: {
      type: Object,
      required: true
    }
  }
}

</script>

<style scoped lang="scss">



</style>