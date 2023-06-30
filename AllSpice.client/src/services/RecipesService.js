import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { Recipe } from '../models/Recipe'


class RecipesService {

  async getAllRecipes() {
    const res = await api.get('api/recipes')
    AppState.recipes = res.data.map(r => new Recipe(r))
    logger.log('[ALL RECIPES] res.data:', res.data)
    logger.log(AppState.recipes)
  }

  async getRecipeById(recipeId) {
    const res = await this.getAllRecipes
  }

  async deleteRecipe(recipeId) {
    await api.delete(`api/recipes/${recipeId}`)
    AppState.recipes = AppState.recipes.filter(r => r.id !== recipeId)
  }







}

export const recipesService = new RecipesService()