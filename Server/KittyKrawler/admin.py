from django.contrib import admin
from .models import GameSave, Leaderboard, Item

class GameSaveAdmin(admin.ModelAdmin):
    model = GameSave
    list_display = ('user', 'get_item_list', 'kill_counter', 'attack', 'defence', 'speed', 'health', 'total_health', 'next_level',
                    'time', 'created', 'current')

    def get_item_list(self, obj):
        items = obj.save_items.all()
        return [item.item_id for item in items]
    get_item_list.short_description = "Items"

class LeaderboardAdmin(admin.ModelAdmin):
    list_display = ('user', 'get_save_created', 'get_save_current')

    def get_save_created(self, obj):
        return obj.game_save.created
    get_save_created.short_description = 'Time Created'

    def get_save_current(self, obj):
        return obj.game_save.current
    get_save_current.short_description = 'Current'

class ItemAdmin(admin.ModelAdmin):
    model = Item
    list_display = ('item_id', 'name')

admin.site.register(GameSave, GameSaveAdmin)
admin.site.register(Leaderboard, LeaderboardAdmin)
admin.site.register(Item, ItemAdmin)


#admin.site.site_header = 'BYTEME Administration'
#admin.site.site_title = 'BYTEME Admin'
#admin.site.index_title = 'BYTEME Admin'