<template>
 
  <div class="offcanvas-body row" v-if="recipe">
    <!-- <div class="card-custom-chef">
      <p>{{ recipe.title }}</p>
    </div> -->
    <div class="card card-custom border-white border-0 my-5" style="max-height: 100vh; min-height: 50vh;">
      <button type="button" class="btn-close bg-light p-3" data-bs-dismiss="offcanvas" data-bs-target="#offcanvasWithBothOptions" aria-label="Close">
      </button>
 
        <div class="card-custom-img m-2" :style="`background-image: url(${recipe.image})`">
          <div style="background-color: #00000095; box-shadow: 0 5px 3px 3px #00000095;">
            <h6 class="card-title text-center"> {{ recipe.title }} </h6>
          </div>
        </div>
          <!-- SECTION * * * CARD BODY * * * --------------->
          <div class="card-body" style="overflow-y: auto">
            
            <p class="card-text"> {{ recipe.category }} </p>
            <!-- <p  v-if="recipe.instructions.length <= 20" class="card-text"></p>
            <div v-if="recipe?.id == account.id">

            </div> -->
            <p class="card-text">{{ recipe.instructions }}</p>
          </div>
          <!-- SECTION * * * CARD FOOTER * * * ---------------------------------------------->
          <div class="card-footer row" style="background: inherit; border-color: inherit;">
            <div class="col-12">
              <p class="card-text">
                <small class="card-text" style="font-weight: 650;">
                  Last Updated on: {{ 
                    new Date( recipe.createdAt )
                    .toLocaleDateString('en-US', {
                      year: 'numeric', 
                      month: 'short', 
                      day: 'numeric'
                    }) }}
                    @ {{ 
                      new Date( recipe.createdAt )
                      .toLocaleTimeString('en-US', {
                        hour: 'numeric', 
                        minute: 'numeric'
                      }) }}
                </small>
              </p>
            </div>
          </div>
        </div>
    
  </div>
  

</template>

<script>

import { computed } from 'vue'
import { AppState } from '../AppState'
// import Pop from "../utils/Pop"
// import { logger } from '../utils/Logger'
// import { recipesService } from '../services/RecipesService.js'
// import { useRoute } from "vue-router"

export default {
  
  setup() {

    
    return {

      account: computed(() => AppState.account),
      recipe: computed(() => AppState.activeRecipe),


    }
  }
}

</script>

<style scoped lang="scss">

.recipe-img {
  aspect-ratio: 1/.5;
  box-shadow: 0 0 15px 15px #000000;
  filter: drop-shadow(0 0 15px 15px #000000);
}

.card-custom {
  color: #efefef;
  text-shadow: 0 1px 5px #998ce2;
  overflow: hidden;
  background-color: #640000f0;
  box-shadow: 0 0 15px 2px #0a0a0a4d;
  transition: .5 ease-in-out;

}

.card-custom-img {
  height: 200px;
  min-height: 200px;
  background-color: #00000090;
  background-repeat: no-repeat;
  background-size: cover;
  background-position: center;
  opacity: .8;
  filter: brightness(1.3);
  box-shadow: 0 10px 0 10px #0a0a0a4d;
  border-color: inherit;
}

/* First border-left-width setting is a fallback */
.card-custom-img::after {
  position: absolute;
  content: '';
  top: 160px;
  left: -10px;
  transform: rotate-X(180deg);
  width: 0;
  height: 0;
  border-style: solid;
  border-top-width: 40px;
  border-right-width: 0;
  border-bottom-width: 0;
  border-left-width: 545px;
  border-right-width: 545px;
  border-top-color: transparent;
  border-right-color: transparent;
  border-bottom-color: transparent;
  border-left-color: #0b0d12ef;
}

.card-custom-chef p {
  background-color: #ffaa64d4;
  border-radius: 5%;
  box-shadow: 0 0 15px 3px #0a0a0a9a;
  position: relative;
  top: 55px;
  left: 5.25rem;
  text-align: center;
  width: 110px;
  height: 33px;
}

.card-custom:hover {
  filter: brightness(1.1);
  transition: .5s ease-in-out;
}

::-webkit-scrollbar {
  background-color: #2d288cc6;
  width: .85rem;
  border-radius: 0.25rem;
}

::-webkit-scrollbar-thumb {
  background-color: #8b8b8b5f;
  border-radius: 0.25rem;
}

::-webkit-scrollbar-track {
  background-color: rgba(32, 20, 12, 0.512);
  border-radius: 0.25rem;
  border-style: ridge;
}

::-webkit-scrollbar-corner {
  background-color: #2d288c;
  border-radius: 0.5rem;
  border-style: groove;
}


</style>