<template>

  <nav class="navbar navbar-expand-lg bg-body-dark" style="opacity: .9;">
    <div class="container-fluid">
      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
          <img alt="logo" src="../assets/img/cw-logo.png" height="45" />
        </div>
      </router-link>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
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
              <li><hr class="dropdown-divider"></li>
              <li><a class="dropdown-item" href="#">Something else here</a></li>
            </ul>
          </li>
          <li class="nav-item">
            <a class="nav-link disabled">Disabled</a>
          </li>
        </ul>
        <form @submit.prevent="searchRecipes()" class="d-flex" role="search">
          <input type="search" class="form-control me-2" placeholder="Search Recipes..." aria-label="Search" v-model="editable">
          <button class="btn btn-outline-success" type="submit">Search</button>
        </form>
      </div>
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

    const editable = ref('')

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
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
