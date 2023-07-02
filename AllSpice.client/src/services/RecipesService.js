import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { Recipe } from '../models/Recipe'
import Pop from "../utils/Pop.js"

class RecipesService {

  async getAllRecipes() {
    const res = await api.get('api/recipes')
    AppState.recipes = res.data.map(r => new Recipe(r))
    logger.log('[ALL RECIPES] res.data:', res.data)
    logger.log(AppState.recipes)
  }

  getRecipeById(recipeId) {
    AppState.activeRecipe = AppState.recipes.find(r => r.id == recipeId)
  }

  async createRecipe(recipeData) {
    logger.log('[GRABBING POST DATA => CREATE RECIPE]', recipeData)
    const res = await api.post('api/recipes', recipeData)
    logger.log(res.data, `${recipeData}`)
    Pop.success(`${res.data.name} has been created!`)
    return res.data
  }

  async deleteRecipe(recipeId) {
    await api.delete(`api/recipes/${recipeId}`)
    AppState.recipes = AppState.recipes.filter(r => r.id !== recipeId)
  }







}

export const recipesService = new RecipesService()