<template>
  <section class="container-fluid">
    <article class="card">
      <img :src="recipes[40].image" class="card-img-top" :alt="recipes[40].title">
      <div class="card-body">
        <h5 class="card-title">{{ recipes[40].title }}</h5>
        <p class="card-text">{{ recipes[40].description }}</p>
      </div>
      <div class="card-footer">
        <small class="text-muted">By {{ recipes[40].creator.name }}</small>
        <button class="btn btn-danger btn-sm float-right" @click="deleteRecipe">Delete</button>
      </div>
    </article>


  </section>
  
    <!--SECTION  * * * CARD IMAGE HEADER * * * ------------------------------>
    <!-- <div class="card card-custom border-white border-0" style="height: 500px"> -->
      <!-- <div class="card-custom-img" :style="`background-image: url(${recipe?.creator.picture})`">
      </div> -->
    <!-- </div> -->




  <!-- <section class="container-fluid">
    <div class="card card-custom border-white border-0" style="height: 500px">
      <div class="card-custom-img" :style="`background-image: url(${tournament.gameImg})`"></div>
      <div v-if="tournament.creatorId == profile.id">
        <h5 class="host-badge">HOST</h5>
      </div>
      <div class="card-custom-avatar">
        <img class="img-fluid object-fit-cover" :src="tournament.creator.picture" :alt="tournament.creator.name" />
      </div> -->
      <!--SECTION * * * CARD BODY * * * 
      <div class="card-body" style="overflow-y: auto">
        <h4 class="card-title"> {{ tournament.name }} </h4>
        <p class="card-text"> {{ tournament.category }} </p>
        <p class="card-text text-end" style="font-weight: 650;">Spots Filled: {{ tournament.participantCount }} / {{ tournament.capacity }} </p>
        <p  v-if="tournament.description.length <= 20" class="card-text">Compete with the best gamers in the valley to take home your pride. And a cash prize!</p>
        <p class="card-text">{{ tournament.description }}</p>
      </div>
 -SECTION * * * CARD FOOTER * * * --------------------------------------------
      <div class="card-footer row" style="background: inherit; border-color: inherit;">
        <div class="col-12">
          <p class="card-text">
            <small class="card-text" style="font-weight: 650;">
              Last Updated on: {{ 
                new Date( tournament.updatedAt )
                .toLocaleDateString('en-US', {
                  year: 'numeric', 
                  month: 'short', 
                  day: 'numeric'
                }) }}
                @ {{ 
                  new Date( tournament.updatedAt )
                  .toLocaleTimeString('en-US', {
                    hour: 'numeric', 
                    minute: 'numeric'
                  }) }}
            </small>
          </p>
        </div>
        <div class="col-6 ps-4 py-1">
          <a href="#" class="btn btn-dark" style="color: dodgerblue;">To Top</a>
        </div>
        <div class="col-6 ps-5 py-1">
          <router-link :to="{ name: 'TournamentDetails', params: { tournamentId: tournament.id } }">
            <a href="#" aria-label="Go to Tournament Page" class="btn btn-outline-primary">Battle</a>
          </router-link>
        </div>
      </div>
    </div>   
</section>  -->
   


</template>

<script>
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { onMounted, computed } from "vue"
import { recipesService } from "../services/RecipesService.js"
import { AppState } from "../AppState.js"
//import RecipeCard from "../components/RecipeCard.vue"

export default {
    name: "HomePage",

   // components: {
 //       RecipeCard
  //  },
    setup() {
        async function getAllRecipes() {
            try {
                logger.log("[GETTING RECIPES]");
                await recipesService.getAllRecipes();
            }
            catch (error) {
                Pop.error(error.message);
                logger.log(error);
            }
        }
        async function getRecipeById(recipeId) {
            try {
                logger.log("[GETTING RECIPE BY ID]");
                await recipesService.getRecipeById(recipeId);
            }
            catch (error) {
                Pop.error(error.message);
                logger.log(error);
            }
        }
        onMounted(() => {
            getAllRecipes();
            getRecipeById();
        });
        return {
            recipes: computed(() => AppState.recipes),
            account: computed(() => AppState.account),
        };
    },
}
</script>

<style scoped lang="scss">

.hero-img {
  height: 500px;
  width: 100%;
  object-fit: cover;
  object-position: center;
}

.recipe-img {
  aspect-ratio: 1/.5;
  box-shadow: 0 0 15px 15px #000000;
  filter: drop-shadow(0 0 15px 15px #000000);
}

.card-custom {
  color: #efefef;
  text-shadow: 0 1px 5px #998ce2;
  overflow: hidden;
  min-height: 450px;
  background-color: #0a0b14f3;
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
  top: 161px;
  left: 0;
  width: 0;
  height: 0;
  border-style: solid;
  border-top-width: 40px;
  border-right-width: 0;
  border-bottom-width: 0;
  border-left-width: 545px;
  border-left-width: calc(575px - 5vw);
  border-top-color: transparent;
  border-right-color: transparent;
  border-bottom-color: transparent;
  border-left-color: #0b0d12ef;
}

.card-custom-chef img {
  border-radius: 50%;
  box-shadow: 0 0 15px 3px #0a0a0a9a;
  position: absolute;
  border: 2.5px solid #000000;
  top: 100px;
  left: 1.25rem;
  width: 100px;
  height: 100px;
}


</style>
