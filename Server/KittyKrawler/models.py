from django.db import models
from django.contrib.auth.models import User


class Item(models.Model):
    item_id = models.IntegerField()
    name = models.CharField(max_length=25)

    def __str__(self):
        return str(self.item_id)

class GameSave(models.Model):
    user = models.ForeignKey(User, related_name='game_save')
    save_items = models.ManyToManyField(Item)

    current = models.BooleanField(default=True)

    created = models.DateTimeField(auto_now_add=True)
    attack = models.IntegerField(null=True)
    defence = models.IntegerField(null=True)
    speed = models.IntegerField(null=True)
    health = models.IntegerField(null=True)
    total_health = models.IntegerField(null=True)
    next_level = models.IntegerField(null=True)
    time = models.DurationField(null=True)
    kill_counter = models.IntegerField(null=True)

class Leaderboard(models.Model):
    user = models.ForeignKey(User)
    game_save = models.OneToOneField(GameSave)