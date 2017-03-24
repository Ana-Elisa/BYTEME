from django.contrib import admin
from .models import GameSave, Leaderboard, Item

class GameSaveAdmin(admin.ModelAdmin):
    model = GameSave
    list_display = ('user', 'attack', 'defence', 'speed', 'health', 'total_health', 'next_level', 'time', 'created', 'current')

class LeaderboardAdmin(admin.ModelAdmin):
    list_display = ('user', 'get_save_created', 'get_save_current')

    def get_save_created(self, obj):
        return obj.game_save.created
    get_save_created.short_description = 'Time Created'

    def get_save_current(self, obj):
        return obj.game_save.current
    get_save_current.short_description = 'Current'


admin.site.register(GameSave, GameSaveAdmin)
admin.site.register(Leaderboard, LeaderboardAdmin)
admin.site.register(Item)