<template>

  <nav class="navbar navbar-expand-lg bg-nav" style="opacity: 1;">
    <div class="container-fluid">
      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
          <img title="home" alt="logo" src="https://i.pinimg.com/originals/20/f9/d0/20f9d0b7e5ace34d9693d81f3201b5c0.png" style="opacity: .9; filter: brightness(.7)" height="45" />
        </div>
      </router-link>

        <!-- ANCHOR - Offcanvas Button for opening the 'SearchRecipesOffcanvas.vue' Component [Located in 'App.vue' - Line 18] -->
      <div class="d-flex justify-content-end pe-5">
        <div class="col-7 me-5">
          <img title="Search Recipes..." src="src/assets/img/hamburger-menu-search.jpeg" class="search-burger justify-content-end align-items-center img-fluid" style="z-index: 1; height: 45px; background-size: cover; object-fit: cover;" role="button" data-bs-toggle="offcanvas" data-bs-target="#myOffcanvas" aria-controls="myOffcanvas">
        </div>
      </div>

      <!-- 
      <form class="d-flex">
        <input type="text" class="form-control me-2" placeholder="Search Recipes..." aria-label="Search" v-model="editable">
        <button @click="" class="btn btn-outline-light mx-2" style="color: aliceblue; text-shadow: 1px 1px 1px black; box-shadow: 1px 1px 3px black; background-color: #f1ff55b8;" type="submit">Search</button>
      </form> 
      -->
        <Login />
    </div>

  </nav>

  <!-- <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="../assets/img/cw-logo.png" height="45" />
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li>
      </ul>
      LOGIN COMPONENT HERE
      
    </div>
  </nav> -->
</template>

<script>
import Login from './Login.vue';
import { logger } from '../utils/Logger'
import { ref } from 'vue'
import { AppState } from '../AppState'
import { recipesService } from '../services/RecipesService'
import Pop from "../utils/Pop"

export default {
  setup() {

    const editable = ref({})

    return {

      editable,

      async searchRecipes() {
        try {
          const searchTerm = editable.value
            .split(' ')
            .join('+')
          logger.log("[SEARCH TERM] =>",searchTerm)
          AppState.query = searchTerm
          await recipesService.searchRecipes(searchTerm)
          editable.value = ''
          // router.push(
          //   { name: 'Search' , params: { query: searchString }}
          // )
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>

.navbar-expand-lg {
  position: fixed;
  width: 100%;
  z-index: 1;
}

.search-burger {
  position: absolute;
  top: 8px;
  right: 150px; 
  box-shadow: 0px 0px 10px rgb(248, 219, 74);
  opacity: .8;
  filter: blur(.1px);
  z-index: 2;
  border-radius: 50%;
  aspect-ratio: 1/1;
  transition: .5s;
}

.search-burger:hover {
  opacity: 1;
  filter: brightness(1.1);
  transform: scale(1.05);
  transition: .5s;
}

a:hover {
  text-decoration: none;
}

.nav-link {
  color: aliceblue;
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  color: aliceblue;
  position: relative;
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.bg-nav {
  background-color: #a73319fb;
  box-shadow: 0px 0px 10px #e43811fb;
}

.button {
  animation: 1.5s ease infinite alternate running shimmer;
  background: linear-gradient(90deg, #FFE27D 0%,
                                     #ffb05b 30%,
                                     #ff916c 85%);
  background-size: 200% 100%;
  border: none;
	border-radius: 6px;
  box-shadow: -2px -2px 10px #ffe37e80, 
              2px 2px 10px #ff620080;
  color: #170F1E;
  cursor: pointer;
	font-family: 'Inter', sans-serif;
  font-size: 16px;
	font-weight: 670;
  line-height: 24px;
  height: 45px;
  overflow: hidden;
  padding: 12px 24px;
  position: absolute;
  text-decoration: none;
  transition: 0.2s;
  
  svg {
    left: -20px;
    opacity: 0.5;
    position: absolute;
    top: -2px;
    transition: 0.5s cubic-bezier(.5,-0.5,.5,1.5);
  }
  
  &:hover svg {
    opacity: 0.8;
    transform: translateX(50px) scale(1.5);
  }
  
  &:hover {
    transform: rotate(-3deg);
  }
  
  &:active {
    transform: scale(0.95) rotate(-3deg);
  }
}

.button:hover {
  animation: 1.5s ease infinite alternate running shimmerAndTilt;
}

@keyframes shimmerAndTilt {
  0% {
    background-size: 100% 100%;
    box-shadow: -2px -2px 6px #ffe37e80, 
                2px 2px 6px #9094ff80;
  }
  50% {
    background-size: 200% 100%;
    box-shadow: -2px -2px 10px #ffe37e80, 
                2px 2px 10px #ff620080;
  }
  100% {
    background-size: 100% 100%;
    box-shadow: -2px -2px 6px #ffe37e80, 
                2px 2px 6px #9094ff80;
  }
}


@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
