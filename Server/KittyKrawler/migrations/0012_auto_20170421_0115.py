# -*- coding: utf-8 -*-
# Generated by Django 1.11 on 2017-04-21 05:15
from __future__ import unicode_literals

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('KittyKrawler', '0011_gamesave_kill_counter'),
    ]

    operations = [
        migrations.RenameField(
            model_name='gamesave',
            old_name='kill_counter',
            new_name='enkill_counter',
        ),
    ]