import { AppState } from '../AppState'
import { Favorite, FavoriteRecipe } from '../models/Favorite'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import Pop from '../utils/Pop'
// import { AppState } from './../AppState';
class FavoritesService {

  async getMyFlavorIts() {
    const res = await api.get('account/favorites')
    AppState.myFlavorIts = res.data.map(f => new FavoriteRecipe(f))
    logger.log('[MY FAVORITE RECIPES] =>', res.data)
  }

  async addFlavorIt(recipeId) {
    if (!AppState.account.id) {
      Pop.toast(`Log in to add this recipe to your favorites!`, 'error')
    } else {
      // const relatedId = AppState.activeRecipe.id
      // AppState.activeRecipe.value = { ...AppState.activeRecipe.value, relatedId }
      const res = await api.post('api/favorites', { recipeId })
      const newFlavorIt = new Favorite(res.data)
      // const foundFlavorIt = res.data.id
      // newFlavorIt.id = foundFlavorIt
      AppState.flavorIts.push(newFlavorIt)
      logger.log('[NEW FAVORITE RECIPE] =>', res.data)
    }
  }

  async removeFlavorIt(recipeId) {

const foundFlavorIt = AppState.myFlavorIts.find(f=>f.id==recipeId)
    const res = await api.delete(`api/favorites/${foundFlavorIt.favoriteId}`)
    logger.log('[REMOVED FAVORITE RECIPE] =>', res.data)
    // const thisFlavorIt = AppState.flavorIts
    const flavorItToRemoveIndex = AppState.myFlavorIts.findIndex(f => f.id === recipeId)
   AppState.myFlavorIts.splice(flavorItToRemoveIndex, 1)
    // AppState.flavorIts = thisFlavorIt
  }
}

export const favoritesService = new FavoritesService()