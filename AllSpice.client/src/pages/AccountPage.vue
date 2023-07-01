<template>
  <section class="container-fluid">
    <nav class="navbar bg-body-tertiary fixed-top">
      <div class="container-fluid">
        <a class="navbar-brand" href="#">Offcanvas navbar</a>
  
          <NavbarHome class="navbar-toggler" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvas" id="offcanvas" aria-controls="offcanvasNavbar" aria-label="Toggle Navigation">
          </NavbarHome>
  
            <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasNavbar" aria-labelledby="offcanvasNavbarLabel">
              <div class="offcanvas-header">
                <h5 class="offcanvas-title" id="offcanvasNavbarLabel">Offcanvas</h5>
                <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
              </div>
              <div class="offcanvas-body">
                <ul class="navbar-nav justify-content-end flex-grow-1 pe-3">
                  <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="#">Home</a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link" href="#">Link</a>
                  </li>
                  <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                      Dropdown
                    </a>
                    <ul class="dropdown-menu">
                      <li><a class="dropdown-item" href="#">Action</a></li>
                      <li><a class="dropdown-item" href="#">Another action</a></li>
                      <li>
                        <hr class="dropdown-divider">
                      </li>
                      <li><a class="dropdown-item" href="#">Something else here</a></li>
                    </ul>
                  </li>
                </ul>
                <form class="d-flex mt-3" role="search">
                  <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
                  <button class="btn btn-outline-success" type="submit">Search</button>
                </form>
             </div>
           </div>
        </div>
      </nav>
  
      <div class="row justify-content-center align-items-center pt-5 mt-5">
          <div class="col-1 d-flex">
              <button class="btn btn-link"
                  onclick="this.parentNode.querySelector('input[type=number]').stepDown()">
                  <i onclick="minusIngredient()" class="mdi mdi-minus"></i>
              </button>
      
                  <input id="form1" min="0" name="quantity" value="0" type="number"
                  class="form-control form-control-sm mb-1" />
      
              <button class="btn btn-link px-1"
                  onclick="this.parentNode.querySelector('input[type=number]').stepUp()">
                  <i onclick="plusIngredient()" class="mdi mdi-plus"></i>
              </button>
          </div>
          <div class="col-11">
              <select name="form-select mb-3">
                  <option :value="i" v-for="i in ingredients" :key="i.id">
                      {{ i.name }}
                  </option>
              </select>
          </div>
      </div>
  </section>
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
import { recipesService } from '../services/RecipesService';
// import NavbarHome from '../components/NavbarHome.vue';
import { logger } from '../utils/Logger';


export default {
  setup() {

    async function getAllIngredients() {
      try {
        logger.log('[GETTING INGREDIENTS]')
        await recipesService.getAllIngredients()
      } catch (error) {
        logger.log(error)
      }
    }

    async function getIngredientById(ingredientId) {
      try {
        logger.log('[GETTING INGREDIENT BY ID]')
        await recipesService.getIngredientById(ingredientId)
      } catch (error) {
        logger.log(error)
      }
    }




// watchEffect(() => {
//   const ingredient = AppState.ingredients.find(i => i.id === AppState.selectedIngredient)
//   if (ingredient) {
//     AppState.recipe.ingredients.push(ingredient)
//   }
// })

// onto the next step, we need to add the quantity to the ingredient, so we need to add a quantity property to the ingredient model

// now that we have the quantity property on the ingredient model, we can add the quantity to the ingredient in the recipe like so without having to add it to the appstate, but by creating an async function in the recipe service that will add the quantity to the ingredient in the recipe and passing it through the return like so after we 'get all ingredients' that we created on our back end



// now we need to write the function on this page to pass down to the service layer like so
    async function addIngredientToRecipe(ingredientData) {
      try {
        await recipesService.addIngredientToRecipe(ingredientData)
      } catch (error) {
        logger.log(error)
      }
    }



    return {
      getAllIngredients,
      getIngredientById,
      addIngredientToRecipe,
      account: computed(() => AppState.account),
      ingredients: computed(() => AppState.ingredients)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
