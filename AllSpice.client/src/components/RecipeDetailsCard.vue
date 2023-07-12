<template>
  <!-- NOTE - OFFCANVAS [START] --------------->
  <div class="offcanvas-body row" v-if="recipe" :key="recipe.id">
    <div class="card card-custom border-white border-0 my-5" style="max-height: 100vh; min-height: 50vh;">
      <button type="button" class="btn-close bg-light p-3" data-bs-dismiss="offcanvas" data-bs-target="#offcanvasWithBothOptions" aria-label="Close" data-bs-toggle="offcanvas">
      </button>
 
        <div class="card-custom-img m-2" :style="`background-image: url(${recipe.image})`">
          <div class="" style="background-color: #00000095; box-shadow: 0 5px 3px 3px #00000095;">
            <div class="row d-flex justify-content-center">
              <h4 class="card-title text-center pt-2"> {{ recipe.title }} </h4>
              <p class="text-end pe-4" style="margin-top: -2rem; color: #ffab3d; font-size: 1.1rem; text-shadow: .5px .5px 1px #fcffc1;">Category: {{ recipe.category }} </p>
            </div>
          </div>
        </div>
          <!-- SECTION * * * CARD BODY * * * --------------->
          <div class="card-body" style="overflow-y: auto">

            <section class="row">
              <div class="col-md-6" style="border-right: 3px ridge black;">
                <div class="">
                  <div class="card-title">
                    <h2 style="text-shadow: 2px 2px 3px black;">Instructions:</h2>
                  </div>
                  <div class="card-body">
                    <h6 v-if="editor == false" class="p-4 rounded-2" style="text-shadow: 1px 1px 3px black; box-shadow: 1px 1px 5px #ff813e; background-color: #5656567d;">{{ recipe.instructions }}</h6>
      
                    <form v-else @submit.prevent="editInstructions()">
                      <label for="instructions">Edit Instructions:</label>
                      <textarea name="instructions" id="instructions" rows="5" class="form-control"
                        v-model="editableSteps.instructions"></textarea>
                      <div class="d-flex justify-content-end">
                        <button class="btn save-edit-button"><i class="mdi mdi-book-edit text-light selectable fs-1" title="Confirm Changes"></i></button>
                      </div>
                    </form>

                    <div class="row d-flex justify-content-end pt-3">
                      <p v-if="isFlavorIt">
                        <i @click="removeFlavorIt(recipe.id)" style="color: #ffab3d;" class="mdi mdi-star text-warning fs-1" title="FlavorIt Recipe"></i>
                      </p>
                      <p v-else>
                        <i @click="addFlavorIt(recipe.id)" style="color: #ffab3d;" class="mdi mdi-star-outline text-warning fs-1" title="Not a FlavorIt Recipe"></i>
                      </p>
                      <button v-if="recipe.creatorId == account.id && !editor" @click.stop="toggle()" type="button" class="edit-button btn">
                        Edit Instructions
                      </button>
                    </div>
                    <div style="position: relative; top: -4.5rem;">
                      <button v-if="recipe.creatorId == account.id && editor" @click.stop="toggle()" style="--c:#E95A49" type="button" class="edit-button btn">
                        Cancel
                       </button>
                    </div>
                  </div>
                </div>
                <div v-if="recipe.creatorId == account.id">
                  <div class="row">
                    <div class="col-12 d-flex justify-content-end align-items-center ">
                      <i @click="deleteRecipe()" class="mdi mdi-trash-can delete-recipe fs-1" style="color: white; z-index: 1;" title="Delete Recipe?">
                      </i>
                    </div>
                  </div>
                </div>
              </div>
      
              <div class="col-md-6">
                <div class="">
                  <div class="card-title">
                    <h2 style="text-shadow: 2px 2px 3px black;">Ingredients</h2>
                  </div>
                  <div class="card-body">

                    <ol>
                      <li v-for="i in ingredients" :key="i.id" class="ingredient-box">
                        <span class="d-flex justify-content-between" style="text-shadow: 1px 1px 1px black;">
                          {{ i.quantity }} {{ i.name }}
                          <span>
                            <i class="mdi mdi-minus text-light selectable"
                              title="Delete Ingredient" v-if="recipe.creatorId == account.id"
                              @click="deleteIngredient(i.id)">
                            </i>
                          </span>
                        </span>
                      </li>
                    </ol>

                    <form v-if="recipe.creatorId == account.id" @submit.prevent="createIngredient()" class="row">
                      <div style="text-shadow: 2px 2px 3px black;" class="col-7 pe-1">
                        <label for="ingredient" class="ps-1">Add Ingredients:</label>
                        <input type="text" id="ingredient" minlength="3" maxlength="25" name="ingredient"
                          class="form-control input-fields" v-model="editable.name" required>
                      </div>
                      <div style="text-shadow: 2px 2px 3px black;" class="col-3 px-1">
                        <label for="quantity">Quantity:</label>
                        <input type="text" id="quantity" minlength="3" maxlength="15" name="quantity"
                          class="form-control input-fields" v-model="editable.quantity" required>
                      </div>
                      <div class="col-1 px-1 pt-2 mt-1">
                        <button class="btn" title="Submit"><i class="mdi mdi-plus-circle fs-1" style="color: #ffaa64d4;"></i></button>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
            </section>
  </div> <!-- NOTE - OFFCANVAS [END] --------------->      

          <!-- TODO - IMPLEMENT CARD FOOTER ONCE CREATEDAT & UPDATEDAT ARE WORKING ------->
          <!-- SECTION * * * CARD FOOTER * * * ------------------------------------------->
          <div class="card-footer row" style="background: inherit; border-color: inherit;">
            <div class="col-12">
              <p class="card-text">
                <small class="card-text" style="font-weight: 650;">
                  Last Updated on: {{ 
                    new Date( recipe.updatedAt )
                    .toLocaleDateString('en-US', {
                      year: 'numeric', 
                      month: 'short', 
                      day: 'numeric'
                    }) }}
                    @ {{ 
                      new Date( recipe.updatedAt )
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

import { computed, watchEffect, ref } from 'vue'
import { AppState } from '../AppState'
import Pop from "../utils/Pop"
import { logger } from '../utils/Logger'
import { recipesService } from '../services/RecipesService.js'
import { ingredientsService } from '../services/IngredientsService.js'
import { favoritesService } from '../services/FavoritesService.js'

export default {
  
  setup() {
    
    const editable = ref({});
    const editableSteps = ref({});
    const isFlavorIt = computed(() => AppState.flavorIts.find(f => f.favoriteId == AppState.activeRecipe.id));

    watchEffect(() => {

      if (AppState.activeRecipe) {
        editableSteps.value = { ...AppState.activeRecipe };
      }
      if (AppState.activeRecipe) {
        editable.value = { ...AppState.activeRecipe };
      }

    });

    return {

      editable,
      editableSteps,
      isFlavorIt,
      ingredients: computed(() => AppState.ingredients),
      account: computed(() => AppState.account),
      editor: computed(() => AppState.editor),
      recipe: computed(() => AppState.activeRecipe),
      ingredient: computed(() => AppState.ingredients),

      async createIngredient() {
        try {
          const ingredient = editable.value;
          await ingredientsService.createIngredient(ingredient);
          await ingredientsService.getIngredientsByRecipeId(AppState.activeRecipe.id);
          editable.value = {};
        }
        catch (error) {
          logger.log(error);
          Pop.error(error.message);
        }
      },

      async editInstructions() {
        try {
          const steps = editableSteps.value;
          await recipesService.editInstructions(steps);
          AppState.editor = false;
        }
        catch (error) {
          logger.log(error);
          Pop.error(error.message);
        }
      },
      async deleteIngredient(ingredientId) {
        try {
          const removing = AppState.ingredients.find(i => i.id == ingredientId);
          AppState.activeIngredient = removing;
          if (await Pop.confirm(`Are you sure you want to delete this ${removing.name}?`, `This will permanently delete '${removing.quantity} of ${removing.name}'...`, 'Confirm', 'Cancel')) {
            await ingredientsService.deleteIngredient(ingredientId);
          }
        }
        catch (error) {
          logger.log(error);
          Pop.error(error.message);
        }
      },
      async deleteRecipe() {
        try {
          const recipe = AppState.activeRecipe;
					if (await Pop.confirm(`Are you sure you want to delete ${recipe.title}?`)) {
             recipesService.deleteRecipe(recipe.id)
          }
					Pop.toast(`Did not delete ${recipe.title}`, 'success')
				} catch (error) {
					logger.error(error)
					Pop.toast(error.message, 'error')
				}
      },

      toggle() {
        AppState.editor = !AppState.editor;
      },

      async addFlavorIt() {
        try {
          await favoritesService.addFlavorIt();
        }
        catch (error) {
          logger.log(error);
          Pop.error(error.message);
        }
      },
      async removeFlavorIt() {
        try {
          
          if (await Pop.confirm(`Are you sure you want to remove ${AppState.activeRecipe.title} from your FlavorIts?`, 'This will remove it from your collection...', 'Remove', 'Cancel')) {
            await favoritesService.removeFlavorIt(isFlavorIt.value);
          }
        }
        catch (error) {
          logger.log(error);
          Pop.error(error.message);
        }
      },
    }
  }
}

</script>

<style scoped lang="scss">


.offcanvas-body {
  min-height: 100vh;
  background-color: #0000000e;
  box-shadow: 0 0 15px 3px #0a0a0a9a;
  border-radius: 5px;
  color: #ffab3d;
}
.recipe-img {
  aspect-ratio: 1/.5;
  box-shadow: 0 0 15px 15px #000000;
  filter: drop-shadow(0 0 15px 15px #000000);
}

.card-custom {
  height: 100vh;
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
  background: transparent;
  background-color: #00000090;
  background-repeat: no-repeat;
  background-size: cover;
  background-position: center;
  opacity: .8;
  filter: brightness(1.3);
  box-shadow: 0 10px 0 10px #0a0a0a4d;
  border-color: inherit;
}

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
  border-left-width: 545px; /* NOTE - THIS FIRST BORDER-LEFT-WIDTH SETTING IS A FALLBACK */
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

.ingredient-box {
  background-color: #ff949490;
  color: #fff2e8;
  text-shadow: 1px 1px #000000;
  border-radius: 5%;
  box-shadow: 0 3px 3px #0a0a0a9a;
  border: 2px ridge black;
  height: 33px;
  padding: .5rem;
  padding-bottom: 2rem;
}
.card-custom:hover {
  filter: brightness(1.1);
  transition: .5s ease-in-out;
}

.delete-recipe {
  position: absolute;
  top: 0;
  right: .5rem;
  text-shadow: 2px 2px 2px black;
  transition: all 0.35s;
}

.delete-recipe:hover {
  position: absolute;
  top: 0;
  right: .5rem;
  text-shadow: 1px 1px 3px black;
  animation: shake 0.5s ease-in-out forwards;
  transition: all 0.35s;
}

@keyframes shake {
  0% { 
    transform: translate(1px, 1px) rotate(0deg); 
  }
  10% { 
    transform: translate(-1px, -2px) rotate(-1deg); 
    color: #ffa600;
  }
  20% { 
    transform: translate(-3px, 0px) rotate(1deg); 
    color: #ff7400;
  }
  30% { 
    transform: translate(3px, 2px) rotate(0deg); 
    color: #ff4d00;
  }
  40% { 
    transform: translate(1px, -1px) rotate(1deg); 
    color: #ff1a00;
  }
  50% { 
    transform: translate(-1px, 2px) rotate(-1deg); 
    color: #ff0000;
  }
  60% { 
    transform: translate(-3px, 1px) rotate(0deg); 
    color: #ff1a00;
  }
  70% { 
    transform: translate(3px, 1px) rotate(-1deg); 
    color: #ff4d00;

  }
  80% { 
    transform: translate(-1px, -1px) rotate(1deg); 
    color: #ff7400;
  }
  90% { 
    transform: translate(1px, 2px) rotate(0deg); 
    color: #ffa400;
  }
  100% { 
    transform: translate(1px, -2px) rotate(-1deg);
    color: #ffa600;
  }
}

.input-fields {
  color: #ffffff;
  text-shadow: 1px 1px 3px black;
  box-shadow: 1px 1px 5px #ff813e;
  background-color: #5656567d;
  margin-top: .5rem;
}

/* .input-fields {
  background-color: #00000095;
  border: 1px solid #ffab3d;
  border-radius: 5px;
  color: #ffab3d;
  text-shadow: 1px 1px 3px black;
  box-shadow: 0 0 15px 3px #0a0a0a9a;
} */

.edit-button {
  --c:  #ffeb67bd; /* NOTE - SETS THE COLOR FOR REUSABLE BUTTON */

  box-shadow: 0 0 0 .1em inset var(--c); 
  --_g: linear-gradient(var(--c) 0 0) no-repeat;
  background: 
    var(--_g) calc(var(--_p,0%) - 100%) 0%,
    var(--_g) calc(200% - var(--_p,0%)) 0%,
    var(--_g) calc(var(--_p,0%) - 100%) 100%,
    var(--_g) calc(200% - var(--_p,0%)) 100%;
  background-color: #ffc6558d;
  background-size: 50.24% calc(var(--_p,0%)/2 + .5%);
  height: 5vh;
  width: 13vw;
  outline-offset: .1em;
  transition: background-size .4s, background-position 0s .4s;
}
.edit-button:hover {
  --_p: 100%;
  color: #000000;
  text-shadow: .5px .5px 1px #ff6363d7;
  transition: background-position .4s, background-size 0s;
}
.edit-button:active {
  box-shadow: 0 0 9e9q inset #0009; 
  background-color: var(--c);
  color: #fff;
}

.edit-button {
  color: #000000;
  text-shadow: .5px .5px 1px #ffffffd7;
  font-family: system-ui, sans-serif;
  font-size: 1rem;
  cursor: pointer;
  padding: .1em .6em;
  font-weight: bold;  
  border: none;
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